using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PlayerEssence.ToolsEssence
{
    public class Tool : MonoBehaviour
    {
        [SerializeField] protected Transform _rotator;
        [SerializeField] protected ToolView _view;

        private Transform _defaultParent;
        
        public UnityAction<Tool> OnSelected;

        protected virtual void Awake()
        {
            if(_defaultParent == null)_defaultParent = transform.parent;
            _view.Init(this);
            _view.OnSelected += OnSelected;
        }

        public void SetParent(Transform parent)
        {
            _defaultParent = parent;
            transform.SetParent(_defaultParent);
        }

        public void ResetUI(Transform rotator, Button buttonOnPanel, Transform placeOnUI,  Transform placeDefaultInHand)
        {
            _rotator = rotator;
            if(_defaultParent == null)_defaultParent = transform.parent;
            _view.UpdateView(buttonOnPanel, placeOnUI, placeDefaultInHand);
        }

        public virtual void Return()
        {
            transform.SetParent(_defaultParent);
            _view.ReturnToUI();
        }

        public virtual void GetInHand()
        {
            _rotator.localRotation = Quaternion.identity;
            transform.SetParent(_rotator);
            _view.GetInHand();
        }

        public virtual void Press(Vector3 direction)
        {
            var currentRotation = _rotator.localRotation;
            _rotator.LookAt(direction);
            var rotation = _rotator.localRotation;
            _rotator.localRotation = Quaternion.Slerp(currentRotation, rotation, Time.deltaTime * 10);
            //var rotation = Quaternion.LookRotation (direction+transform.position, Vector3.up);
            // rotation.x = 0; This is for limiting the rotation to the y axis. I needed this for my project so just
            // rotation.z = 0;                 delete or add the lines you need to have it behave the way you want.
            //_rotator.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * 100);
            //_rotator.LookAt(direction);
        }

        public virtual void Click(Vector3 direction)
        {
            //var rotation = Quaternion.LookRotation (direction);
            // rotation.x = 0; This is for limiting the rotation to the y axis. I needed this for my project so just
            // rotation.z = 0;                 delete or add the lines you need to have it behave the way you want.
            //_rotator.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * 100);
            
            //_rotator.LookAt(direction);
        }

        private void OnDestroy()
        {
            _view.OnSelected -= OnSelected;
        }
    }
}