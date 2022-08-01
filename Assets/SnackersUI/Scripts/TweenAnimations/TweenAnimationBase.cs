using DG.Tweening;
using UnityEngine;

namespace SnackersUI
{
    public abstract class TweenAnimationBase : MonoBehaviour
    {
        [Header("Time")]
        [SerializeField] protected float _duration = 0.3f;
        [SerializeField] protected float _delay;

        [Header("Ease")]
        [SerializeField] protected Ease _ease = DOTween.defaultEaseType;

        public abstract void Play();

#if UNITY_EDITOR
        public abstract string GetDescription();
#endif
    }
}