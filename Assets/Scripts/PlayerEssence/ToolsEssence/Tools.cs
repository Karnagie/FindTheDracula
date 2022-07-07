using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerEssence.ToolsEssence
{
    public class Tools : MonoBehaviour
    {
        [SerializeField] private Transform _rotator;
        [SerializeField] private Button _buttonOnPanel;
        [SerializeField] private Transform _placeOnUI;
        [SerializeField] private Transform _placeDefaultInHand;
        
        [SerializeField] private Transform _weaponParent;
        [SerializeField] private List<Tool> _tools;

        public Tool Current { get; private set; }

        public void Init()
        {
            _weaponParent = transform;
            Current = _tools[0];
            foreach (var tool in _tools)
            {
                tool.OnSelected += OnSelectedTools;
            }

            OnSelectedTools(_tools[0]);
        }

        public void AddWeapon(Tool newTool)
        {
            newTool.OnSelected += OnSelectedTools;
            Vector3 position = newTool.transform.position;
            newTool.transform.SetParent(_weaponParent, _weaponParent);
            newTool.transform.localPosition = position;
            newTool.ResetUI(_rotator, _buttonOnPanel, _placeOnUI, _placeDefaultInHand);
            _tools.Add(newTool);
        }
        
        public void TurnOff()
        {
            foreach (var tool in _tools)
            {
                tool.OnSelected -= OnSelectedTools;
            }
        }

        private void OnSelectedTools(Tool tool)
        {
            Current.Return();
            Current = tool;
            Current.GetInHand();
        }

        public void OnClick(Vector3 direction)
        {
            Current.Click(direction);
        }

        public void Pressing(bool pressing, Vector3 direction)
        {
            if (pressing) Current.Press(direction);
        }
    }
}