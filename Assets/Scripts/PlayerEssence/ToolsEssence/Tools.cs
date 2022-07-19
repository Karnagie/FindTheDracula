using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerEssence.ToolsEssence
{
    public class Tools : MonoBehaviour
    {
        [SerializeField] private GameObject _toolsParent;
        [SerializeField] private GameObject _toolCanvas;
        
        [SerializeField] private Transform _rotator;
        [SerializeField] private Button _buttonOnPanel;
        [SerializeField] private Button _buttonOnPanelTool;
        [SerializeField] private Transform _placeOnUI;
        [SerializeField] private Transform _placeOnUITool;
        [SerializeField] private Transform _placeDefaultInHand;
        [SerializeField] private Transform _placeDefaultInHandTool;
        
        [SerializeField] private Transform _weaponParent;
        [SerializeField] private Transform _toolParent;
        [SerializeField] private List<Tool> _tools;

        public Tool Current { get; private set; }

        public void Init()
        {
            _buttonOnPanelTool.gameObject.SetActive(false);
            _buttonOnPanel.onClick.AddListener((() =>
            {
                _buttonOnPanelTool.gameObject.SetActive(true);
                _buttonOnPanel.gameObject.SetActive(false);
            }));
            _buttonOnPanelTool.onClick.AddListener((() =>
            {
                _buttonOnPanel.gameObject.SetActive(true);
                _buttonOnPanelTool.gameObject.SetActive(false);
            }));
            
            _tools = new List<Tool>();
            //_weaponParent = transform;
            // Current = _tools[0];
            // foreach (var tool in _tools)
            // {
            //     tool.OnSelected += OnSelectedTools;
            // }

            //OnSelectedTools(_tools[0]);
            _toolsParent.SetActive(true);
            _toolCanvas.SetActive(true);
        }
        
        public void AddEquipment(Tool newTool)
        {
            newTool.OnSelected += OnSelectedTools;
            Vector3 position = newTool.transform.position;
            newTool.SetParent(_toolParent);
            newTool.transform.localPosition = position;
            if (newTool.ChangePlace)
            {
                newTool.ResetUI(_rotator, _buttonOnPanelTool, _placeOnUITool, _placeDefaultInHand, false);
            }else newTool.ResetUI(_rotator, _buttonOnPanelTool, _placeOnUITool, _placeDefaultInHandTool, false);
            newTool.Return();
            _tools.Add(newTool);
            _tools[0].GetInHand();
            Current = _tools[0];
        }

        public void AddWeapon(Tool newTool)
        {
            newTool.OnSelected += OnSelectedTools;
            Vector3 position = newTool.transform.position;
            newTool.SetParent(_weaponParent);
            newTool.transform.localPosition = position;
            newTool.ResetUI(_rotator, _buttonOnPanel, _placeOnUI, _placeDefaultInHand, true);
            newTool.Return();
            _tools.Add(newTool);
        }
        
        public void TurnOff()
        {
            foreach (var tool in _tools)
            {
                tool.OnSelected -= OnSelectedTools;
            }
            
            _toolsParent.SetActive(false);
            _toolCanvas.SetActive(false);
        }

        private void OnSelectedTools(Tool tool)
        {
            Current.Return();
            Current = tool;
            Current.GetInHand();
        }

        public void OnClick(Vector3 direction)
        {
            if (Current == null)
            {
                OnSelectedTools(_tools[0]);
            }
            Current.Click(direction);
        }

        public void Pressing(bool pressing, Vector3 direction)
        {
            if (pressing) Current.Press(direction);
        }
    }
}