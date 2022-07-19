using System;
using Core.InputEssence;
using Core.RayCastingEssence;
using PlayerEssence.ToolsEssence;
using UnityEngine;
using Zenject;

namespace PlayerEssence.FlashlightEssence
{
    public class Flashlight : Tool
    {
        [SerializeField] private FlashlightLight _light;
        
        [Inject] private IInputSystem _input;

        private void Start()
        {
            //_light.TurnOff();
        }

        public override void Return()
        {
            base.Return();
            _light.TurnOff();
        }

        public override void GetInHand()
        {
            base.GetInHand();
            _light.TurnOn();
        }

        public override void Press(Vector3 direction)
        {
            var camDirection = Camera.main.ScreenPointToRay(_input.MousePosition);
            if (Physics.Raycast(camDirection, out var hit, 100, LayerMask.GetMask("Stuff")))
            {
                transform.LookAt(hit.point);
            }
            else
                transform.LookAt(transform.position+camDirection.direction);
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
        }
    }
}