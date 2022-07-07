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
        
        [Inject] private IInputSystem _input;
        [Inject] private RayCasting _rayCasting;

        private void CreateBullet(AliveObject mVampire)
        {
            Bullet bullet = Instantiate(_bulletPrefab, _startBulletPosition.position,
                quaternion.identity);
            bullet.transform.LookAt(mVampire.Heart);
            bullet.MoveTo(mVampire);
        }
        
        public override void Press(Vector3 direction)
        {
            var camDirection = Camera.main.ScreenPointToRay(_input.MousePosition);
            if (Physics.Raycast(camDirection, out var hit, 100, LayerMask.GetMask("Stuff")))
            {
                transform.LookAt(hit.point);
            }
            else
                transform.LookAt(camDirection.direction*10);
        }

        public override void Click(Vector3 direction)
        {
            var camDirection = Camera.main.ScreenPointToRay(_input.MousePosition);
            if (Physics.Raycast(camDirection, out var hit, 100, LayerMask.GetMask("Stuff")))
            {
                transform.LookAt(hit.point);
            }
            else
                transform.LookAt(camDirection.direction*10);
            
            AliveObject[] aliveObjects = _rayCasting.CastAll<AliveObject>();
            if (aliveObjects.Length >= 1)
            {
                foreach (var aliveObject in aliveObjects)
                {
                    CreateBullet(aliveObject);
                }
            }
        }
    }
}