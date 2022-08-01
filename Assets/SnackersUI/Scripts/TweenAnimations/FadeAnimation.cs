using DG.Tweening;
using UnityEngine;

namespace SnackersUI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeAnimation : TweenAnimationBase
    {
        [Header("Fade")]
        [SerializeField, Range(0, 1)] private float _targetOpacity = 1f;
        [SerializeField] private bool _setInitial = false;
        [SerializeField, Range(0, 1)] private float _initialOpacity = 0f;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            if (_setInitial)
                _canvasGroup.alpha = _initialOpacity;
        }

        public override void Play()
        {
            if (_setInitial) _canvasGroup.alpha = _initialOpacity;
            DOTween.Kill(_canvasGroup);
            _canvasGroup.DOFade(_targetOpacity, _duration)
                .SetDelay(_delay)
                .SetEase(_ease);
        }

#if UNITY_EDITOR
        public override string GetDescription()
        {
            var text = $"Opacity → {_targetOpacity} in {_duration}s";
            if (_delay > 0) 
                text += $" delayed {_delay}s";
            return text;
        }
#endif
    }
}