using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.InputEssence
{
    public class PcInputSystem : IInputSystem, IDisposable
    {
        private MainActions _actions;

        public PcInputSystem()
        {
            _actions = new MainActions();
            _actions.Player.Click.started += (ctx => OnStartClick?.Invoke(ctx));
            _actions.Player.Click.canceled += (ctx => OnEndClick?.Invoke(ctx));
            _actions.Player.Tap.performed += (ctx => OnTap?.Invoke(ctx));
            _actions.Enable();
        }
        
        public Action<InputAction.CallbackContext> OnStartClick { get;  set; }
        public Action<InputAction.CallbackContext> OnEndClick { get;  set; }
        public Action<InputAction.CallbackContext> OnTap { get;  set; }

        public Vector3 MousePosition => CalculatePosition();

        private Vector3 CalculatePosition()
        {
            return Touchscreen.current.position.ReadUnprocessedValue();
            return Mouse.current.position.ReadUnprocessedValue();
        }

        public Vector2 MouseDelta => _actions.Player.Delta.ReadValue<Vector2>();

        public void Dispose()
        {
            _actions.Dispose();
        }
    }
}