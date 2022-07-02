using System;
using UnityEngine;
using UnityEngine.Events;

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
            _defaultParent = transform.parent;
            _view.Init(this);
            _view.OnSelected += OnSelected;
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
            _rotator.LookAt(direction);
        }

        public virtual void Click(Vector3 direction)
        {
            _rotator.LookAt(direction);
        }

        private void OnDestroy()
        {
            _view.OnSelected -= OnSelected;
        }
    }
}