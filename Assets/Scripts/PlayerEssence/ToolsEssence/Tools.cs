using System;
using UnityEngine;

namespace PlayerEssence.ToolsEssence
{
    public class Tools : MonoBehaviour
    {
        [SerializeField] private Tool[] _tools;

        public Tool Current { get; private set; }

        public void Init()
        {
            Current = _tools[0];
            foreach (var tool in _tools)
            {
                tool.OnSelected += OnSelectedTools;
            }

            OnSelectedTools(_tools[0]);
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
            foreach (var tool1 in _tools)
            {
                if (tool1 != tool)
                {
                    tool1.Return();
                }
                else
                {
                    Current = tool1;
                    tool1.GetInHand();
                }
            }
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