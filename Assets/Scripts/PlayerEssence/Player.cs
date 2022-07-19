using System;
using System.Threading.Tasks;
using Core.AnimationEssence;
using Core.InputEssence;
using Core.RayCastingEssence;
using DG.Tweening;
using PlayerEssence.ChoosableEquipmentEssence;
using PlayerEssence.ToolsEssence;
using UnityEngine;
using UnityEngine.InputSystem;
using WeaponsEssence;
using Zenject;

namespace PlayerEssence
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform _startPosition;
        [SerializeField] private Vector2 _rectSize;
        [SerializeField] private float _maxRotateAngle;
        [SerializeField] private Tools _tools;
        [SerializeField] private bool _isWorking;
        [SerializeField] private bool _inverseRotate;

        [Inject] private IInputSystem _input;
        [Inject] private RayCasting _rayCasting;
        [Inject] private WeaponInventory _inventory;
        [Inject] private ChoosableEquipmentInventory _equipmentInventory;

        private float _maxAngle;
        private float _startRotateAngle;
        private bool _pressing;
        private float _mouseDelta;
        private float _deltaAngle;

        public void TurnOffControl()
        {
            _isWorking = false;
        }
        
        [ContextMenu("Turn on control")]
        public void TurnOnControl()
        {
            _tools.AddEquipment(_equipmentInventory.GetCurrent());
            _tools.AddWeapon(_inventory.GetCurrent());
            CalculateMaxAngle();
            _startRotateAngle = transform.eulerAngles.y;
            _deltaAngle = 0;
            _isWorking = true;
        }
        
        public void TurnOffUI()
        {
            _tools.TurnOff();
        }

        private void Awake()
        {
            _input.OnStartClick += OnStartClick;
            _input.OnTap += OnTap;
            _input.OnEndClick += OnEndClick;
            //CalculateMaxAngle();
            //_startRotateAngle = transform.localEulerAngles.y;
        }

        private void OnDisable()
        {
            _input.OnStartClick -= OnStartClick;
            _input.OnTap -= OnTap;
            _input.OnEndClick -= OnEndClick;
        }

        private void Start()
        {
            transform.position = _startPosition.position;
            transform.rotation = _startPosition.rotation;
        }

        private void OnTap(InputAction.CallbackContext obj)
        {
            Vector3 direction = Camera.main.ScreenPointToRay(_input.MousePosition).direction.normalized;
            if (_rayCasting.CastAll<ToolButton>().Length == 0 && _isWorking && _input.MousePosition.y > (Screen.height / 6)) _tools.OnClick(transform.position + direction);
        }

        private void OnStartClick(InputAction.CallbackContext obj)
        {
            _pressing = true;
        }
        
        private void OnEndClick(InputAction.CallbackContext obj)
        {
            _mouseDelta = 0;
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
            if(_isWorking)
            {
                Quaternion oldRot = transform.localRotation;
                Vector3 direction = Camera.main.ScreenPointToRay(_input.MousePosition).direction.normalized;
                float rot = Mathf.Abs(RotateAngle()) > _maxAngle ? 1 : 0;
                float rotVel = RotateAngle() > 0 ? -1 : 1;
                transform.Rotate(new Vector3(0, rot * rotVel * Time.deltaTime * 50, 0));
                Vector3 rotation = transform.eulerAngles;
                if (_input.MouseDelta.x != 0) _mouseDelta = _input.MouseDelta.x;
                if (_input.MouseDelta.x <= 0.1f) _mouseDelta = 0;
                rotation.y += _mouseDelta*Time.deltaTime*2;
                // int rotated = 0;
                // if (rotation.y < 55)
                // {
                //     if (_startRotateAngle < 55) _startRotateAngle+= 180;
                //     if (_startRotateAngle > (310))_startRotateAngle -= 180;
                //     rotation.y += 180;
                //     rotated = 180;
                // }
                // if (rotation.y > (310))
                // {
                //     if (_startRotateAngle < 55) _startRotateAngle+= 180;
                //     if (_startRotateAngle > (310))_startRotateAngle -= 180;
                //     rotation.y -= 180;
                //     rotated = -180;
                // }
                _deltaAngle += rot * rotVel * Time.deltaTime * 50 + _mouseDelta*Time.deltaTime*2;
                transform.localRotation = Quaternion.Euler(rotation);
                if (Mathf.Abs(_deltaAngle) > _maxRotateAngle)
                {
                    _deltaAngle -= rot * rotVel * Time.deltaTime * 50 + _mouseDelta*Time.deltaTime*2;
                    transform.localRotation = oldRot;
                }
                //if (rotation.y > _startRotateAngle + _maxRotateAngle) rotation.y = _startRotateAngle + _maxRotateAngle;
                //if (rotation.y < _startRotateAngle - _maxRotateAngle) rotation.y = _startRotateAngle - _maxRotateAngle;
                //if (rotated != 0) rotation.y += rotated;
                //transform.localRotation = Quaternion.Euler(rotation);

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
            
            angle = _input.MousePosition.y > (Screen.height / 6) ? angle : 0;
            
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
                if(_inverseRotate)
                    await transform.DORotate(Quaternion.Euler(0, 0, 0).eulerAngles, 2f)
                        .AsyncWaitForCompletion();
                else
                    await transform.DORotate(Quaternion.Euler(0, _startRotateAngle, 0).eulerAngles, 2f)
                    .AsyncWaitForCompletion();
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        }
    }
}