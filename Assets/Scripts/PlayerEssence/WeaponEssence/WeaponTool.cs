using System.Collections;
using System.Threading.Tasks;
using AliveEssence;
using Core.InputEssence;
using Core.RayCastingEssence;
using PlayerEssence.ToolsEssence;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace PlayerEssence.WeaponEssence
{
    public class WeaponTool : Tool
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _startBulletPosition;
        [SerializeField] private bool _ray;
        
        [Inject] private IInputSystem _input;
        [Inject] private RayCasting _rayCasting;

        private bool _shooting;
        private Vector3 _point;

        private async void CreateBullet(AliveObject mVampire)
        {
            Bullet bullet = Instantiate(_bulletPrefab, _startBulletPosition.position,
                quaternion.identity);
            bullet.transform.LookAt(mVampire.Heart);
            if (_ray)
            {
                bullet.transform.SetParent(_startBulletPosition);
                //Vector3 scale = bullet.transform.localScale;
                //scale.y = (mVampire.Heart - _startBulletPosition.position).magnitude/2;
                bullet.transform.localPosition = Vector3.zero;//bullet.transform.position;//.forward * scale.y;
                bullet.transform.localRotation = _bulletPrefab.transform.localRotation;
                //bullet.transform.localScale = scale;
                //bullet.transform.Rotate(0, -90, -90);
                //bullet.transform.SetParent(transform);
                StartCoroutine(Destroying(bullet.gameObject));
            }else
                bullet.MoveTo(mVampire);
        }

        public IEnumerator Destroying(GameObject bullet)
        {
            _shooting = true;
            yield return new WaitForSeconds(2f);
            _shooting = false;
            Destroy(bullet);
        }

        public override void Press(Vector3 direction)
        {
            if (_shooting)
            {
                transform.LookAt(_point);
                return;
            }
            var camDirection = Camera.main.ScreenPointToRay(_input.MousePosition);
            if (Physics.Raycast(camDirection, out var hit, 100, LayerMask.GetMask("Stuff")))
            {
                transform.LookAt(hit.point);
            }
            else
                transform.LookAt(transform.position+camDirection.direction);
        }

        public override void Click(Vector3 direction)
        {
            var camDirection = Camera.main.ScreenPointToRay(_input.MousePosition);
            if (Physics.Raycast(camDirection, out var hit, 100, LayerMask.GetMask("Stuff")))
            {
                transform.LookAt(hit.point);
            }
            else
                transform.LookAt(transform.position+camDirection.direction);
            
            AliveObject[] aliveObjects = _rayCasting.CastAll<AliveObject>();
            if (aliveObjects.Length >= 1)
            {
                foreach (var aliveObject in aliveObjects)
                {
                    if(aliveObject.Dead) continue;
                    
                    _point = aliveObject.Heart;
                    CreateBullet(aliveObject);
                    if(_ray)aliveObject.Die(2);
                    return;
                }
            }
            // AliveObject[] aliveObjects = _rayCasting.CastAll<AliveObject>();
            // if (aliveObjects.Length >= 1)
            // {
            //     foreach (var aliveObject in aliveObjects)
            //     {
            //         _point = aliveObject.Heart;
            //         CreateBullet(aliveObject);
            //         if(_ray)aliveObject.Die();
            //     }
            // }
        }
    }
}