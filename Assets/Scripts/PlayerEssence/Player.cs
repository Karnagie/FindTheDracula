using System;
using System.Threading.Tasks;
using Core.InputEssence;
using Core.RayCastingEssence;
using DG.Tweening;
using PlayerEssence.ToolsEssence;
using UnityEngine;
using UnityEngine.InputSystem;
using WeaponsEssence;
using Zenject;

namespace PlayerEssence
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Vector2 _rectSize;
        [SerializeField] private float _maxRotateAngle;
        [SerializeField] private Tools _tools;

        [Inject] private IInputSystem _input;
        [Inject] private RayCasting _rayCasting;
        [Inject] private WeaponInventory _inventory;

        private float _maxAngle;
        private float _startRotateAngle;
        private bool _pressing;
        private bool _isWorking = true;

        public void TurnOffControl()
        {
            _isWorking = false;
        }

        private void Awake()
        {
            _tools.AddWeapon(_inventory.GetCurrent());
            _input.OnStartClick += OnStartClick;
            _input.OnTap += OnTap;
            _input.OnEndClick += OnEndClick;
            CalculateMaxAngle();
            _startRotateAngle = transform.localEulerAngles.y;
        }

        private void OnTap(InputAction.CallbackContext obj)
        {
            Vector3 direction = Camera.main.ScreenPointToRay(_input.MousePosition).direction.normalized;
            if (_rayCasting.CastAll<ToolButton>().Length == 0) _tools.OnClick(transform.position + direction);
        }

        private void OnStartClick(InputAction.CallbackContext obj)
        {
            _pressing = true;
        }
        
        private void OnEndClick(InputAction.CallbackContext obj)
        {
            _pressing = false;
        }

        private void CalculateMaxAngle()
        {
            float a = _rectSize.x/2;
            float b = (transform.position - (transform.position - Vector3.forward)).magnitude;
            float c = Mathf.Sqrt(a*a + b*b);
            _maxAngle = Mathf.Asin((a / c))*Mathf.Rad2Deg;
        }

        private void Update()
        {
            if(_isWorking){
                Vector3 direction = Camera.main.ScreenPointToRay(_input.MousePosition).direction.normalized;
                float rot = Mathf.Abs(RotateAngle()) > _maxAngle ? 1 : 0;
                float rotVel = RotateAngle() > 0 ? -1 : 1;
                transform.Rotate(new Vector3(0, rot * rotVel * Time.deltaTime * 50, 0));
                Vector3 rotation = transform.localEulerAngles;
                if (rotation.y > _startRotateAngle + _maxRotateAngle) rotation.y = _startRotateAngle + _maxRotateAngle;
                if (rotation.y < _startRotateAngle - _maxRotateAngle) rotation.y = _startRotateAngle - _maxRotateAngle;
                transform.localRotation = Quaternion.Euler(rotation);

                _tools.Pressing(_pressing, transform.position + direction);
            }
        }

        private float RotateAngle()
        {
            //transform.Rotate(new Vector3(RotateAngle(), 0, 0));
            Vector3 direction = Camera.main.ScreenPointToRay(_input.MousePosition).direction.normalized;
            direction.y = 0;
            float angle = Vector3.Angle(transform.forward.normalized, direction);
            
            Vector3 vector = Quaternion.AngleAxis(-45, Vector3.up) * transform.forward.normalized;
            
            angle = _input.MousePosition.y > (Screen.height / 4) ? angle : 0;
            
            return Vector3.Angle(vector, direction) < 45 ? angle : -angle;
        }

        private void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Vector3.forward, _rectSize);
            if(_input == null) return;
            Gizmos.color = Color.blue;
            Ray cam = Camera.main.ScreenPointToRay(_input.MousePosition);
            Gizmos.DrawLine(Vector3.zero, -cam.direction*10);
        }

        public async Task ReturnToPosition()
        {
            try
            {
                await transform.DORotate(Quaternion.Euler(0, _startRotateAngle, 0).eulerAngles, 0.5f)
                    .AsyncWaitForCompletion();
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        }
    }
}