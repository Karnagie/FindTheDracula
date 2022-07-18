using System;
using Core.RaycastingEssence;
using DG.Tweening;
using UnityEngine;

namespace PlayerEssence.ToolsEssence
{
    public class ToolButton : MonoBehaviour, IRaycastTarget
    {
        private void Start()
        {
            var scale = transform.localScale;
            //transform.localScale = Vector3.zero;
            transform.DOPunchScale(scale/7, 1);
        }

        public Action OnStartClick { get; }
        public Action OnEndClick { get; }
    }
}