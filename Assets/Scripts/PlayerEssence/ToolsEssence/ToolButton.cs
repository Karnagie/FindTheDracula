using System;
using Core.RaycastingEssence;
using UnityEngine;

namespace PlayerEssence.ToolsEssence
{
    public class ToolButton : MonoBehaviour, IRaycastTarget
    {
        public Action OnStartClick { get; }
        public Action OnEndClick { get; }
    }
}