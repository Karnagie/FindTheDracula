using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.InputEssence
{
    public interface IInputSystem : IDisposable
    {
        Action<InputAction.CallbackContext> OnStartClick { get; set; }
        Action<InputAction.CallbackContext> OnEndClick { get; set; }
        
        Vector3 MousePosition { get; }
        
        Vector2 MouseDelta { get; }
    }
}