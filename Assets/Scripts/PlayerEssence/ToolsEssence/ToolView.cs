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
        
        private Tool _tool;
        private Vector3 _defaultRotation;
        
        public UnityAction<Tool> OnSelected;

        private void Awake()
        {
            _defaultRotation = transform.localRotation.eulerAngles;
        }

        public void Init(Tool tool)
        {
            _tool = tool;
            _buttonOnPanel.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _buttonOnPanel.onClick.RemoveListener(OnClick);
        }
        
        private void OnClick()
        {
            _tool.OnSelected?.Invoke(_tool);
        }

        public void ReturnToUI()
        {
            transform.DOKill(false);
            transform.DOLocalMove(_placeOnUI.localPosition, 1);
            transform.DOLocalRotate(_defaultRotation, 0.2f);
            transform.DOScale(_scaleOnUI, 1);
        }

        public void GetInHand()
        {
            _placeDefaultInHand.localRotation = Quaternion.identity;
            transform.DOKill(false);
            transform.DOLocalMove(_placeDefaultInHand.localPosition, 1);
            transform.DOLocalRotate(_defaultRotation, 0.2f);
            transform.DOScale(_scaleInHand, 1);
        }
    }
}