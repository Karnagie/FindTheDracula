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

        private async void CreateBullet(AliveObject mVampire)
        {
            Bullet bullet = Instantiate(_bulletPrefab, _startBulletPosition.position,
                quaternion.identity);
            bullet.transform.LookAt(mVampire.Heart);
            if (_ray)
            {
                Vector3 scale = bullet.transform.localScale;
                scale.y = (mVampire.Heart - _startBulletPosition.position).magnitude/2;
                bullet.transform.position += bullet.transform.forward * scale.y;
                bullet.transform.localScale = scale;
                bullet.transform.Rotate(0, -90, -90);
                bullet.transform.SetParent(transform);
                StartCoroutine(Destroying(bullet.gameObject));
            }else
                bullet.MoveTo(mVampire);
        }

        public IEnumerator Destroying(GameObject bullet)
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(bullet);
        }

        public override void Press(Vector3 direction)
        {
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
                    CreateBullet(aliveObject);
                    if(_ray)aliveObject.Die();
                }
            }
        }
    }
}