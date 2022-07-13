using System;
using Core.RaycastingEssence;
using UnityEngine;

namespace PuzzleEssence.Lock
{
    public class Lockpick : MonoBehaviour,IRaycastTarget
    {
        [SerializeField] private Animation _animation;

        public event Action OnAnimationEnd;

        public void Open()
        {
            _animation.Play();
        }

        public void End()
        {
            OnAnimationEnd?.Invoke();
        }

        public Action OnStartClick { get; }
        public Action OnEndClick { get; }
    }
}