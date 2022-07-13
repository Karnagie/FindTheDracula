using System;
using Core.RaycastingEssence;
using UnityEngine;

namespace PuzzleEssence.Anvil
{
    public class Anvil : MonoBehaviour,IRaycastTarget
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
            _animation["Anvil"].speed = 0;
        }

        public Action OnStartClick { get; }
        public Action OnEndClick { get; }
    }
}