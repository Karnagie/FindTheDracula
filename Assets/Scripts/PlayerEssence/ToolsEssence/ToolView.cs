using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PlayerEssence.ToolsEssence
{
    public class ToolView : MonoBehaviour
    {
        [SerializeField] private Button _buttonOnPanel;
        [SerializeField] private Transform _placeOnUI;
        [SerializeField] private Transform _placeDefaultInHand;
        [SerializeField] private Vector3 _scaleInHand;
        [SerializeField] private Vector3 _scaleOnUI;
        [SerializeField] private Vector3 _rotationOnUI;
        [SerializeField] private Vector3 _defaultRotation;
        [SerializeField] private float _upper = 0.1f;
        
        private Tool _tool;
        private Vector3 _defaultPosition;
        
        public UnityAction<Tool> OnSelected;

        private void Awake()
        {
            _defaultPosition = transform.localPosition;
            transform.localRotation = Quaternion.Euler(_rotationOnUI);
            var scale = transform.localScale;
            //transform.localScale = Vector3.zero;
            transform.DOPunchScale(scale/7, 1);
        }

        public void UpdateView(Button buttonOnPanel, Transform placeOnUI,  Transform placeDefaultInHand)
        {
            _defaultRotation = Quaternion.identity.eulerAngles;
            transform.localRotation = Quaternion.Euler(_rotationOnUI);
            _buttonOnPanel = buttonOnPanel;
            _placeOnUI = placeOnUI;
            _placeDefaultInHand = placeDefaultInHand;
            _buttonOnPanel?.onClick.RemoveListener(OnClick);
            _buttonOnPanel?.onClick.AddListener(OnClick);
            _defaultPosition = transform.localPosition;
        }

        public void Init(Tool tool)
        {
            _tool = tool;
            _buttonOnPanel?.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _buttonOnPanel?.onClick.RemoveListener(OnClick);
        }
        
        private void OnClick()
        {
            _tool.OnSelected?.Invoke(_tool);
        }

        public void ReturnToUI()
        {
            transform.DOKill(false);
            transform.DOLocalMove(_defaultPosition, 1);
            if(transform.localRotation.eulerAngles != _rotationOnUI)transform.DOLocalRotate(_rotationOnUI, 1f);
            transform.DOScale(_scaleOnUI, 1);
        }

        public void GetInHand()
        {
            _placeDefaultInHand.localRotation = Quaternion.identity;
            transform.DOKill(false);
            transform.DOLocalMove(_placeDefaultInHand.localPosition+Vector3.up*_upper, 1);
            transform.DOLocalRotate(_defaultRotation, 0.2f);
            transform.DOScale(_scaleInHand, 1);
        }
    }
}