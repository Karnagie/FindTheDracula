using DG.Tweening;
using UnityEngine;

namespace SnackersUI
{
    [RequireComponent(typeof(RectTransform))]
    public class ScaleAnimation : TweenAnimationBase
    {
        [Header("Scale")]
        [SerializeField] private Vector3 _targetScale = Vector3.one;
        [SerializeField] private float _targetScaleMultiplier = 0.9f;
        [SerializeField] private bool _from = false;

        private RectTransform Rt => GetComponent<RectTransform>();
        private Vector3 TargetScale => _targetScale * _targetScaleMultiplier;

        private Vector3 _initialScale;

        private void Awake()
        {
            _initialScale = Rt.localScale;
        }

        public override void Play()
        {
            DOTween.Kill(Rt);
            if (_from)
            {
                Rt.localScale = TargetScale;
                Rt.DOScale(_initialScale, _duration)
                    .SetDelay(_delay)
                    .SetEase(_ease);
            }
            else
            {
                Rt.DOScale(TargetScale, _duration)
                    .SetDelay(_delay)
                    .SetEase(_ease);
            }
        }

#if UNITY_EDITOR
        public override string GetDescription()
        {
            var text = $"Scale {(_from ? "from" : "to")} {TargetScale} in {_duration}s";
            if (_delay > 0)
            {
                text += $" delayed {_delay}s";
            }

            return text;
        }
#endif
    }
}