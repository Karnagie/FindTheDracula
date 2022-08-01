using DG.Tweening;
using UnityEngine;

namespace SnackersUI
{
    [RequireComponent(typeof(RectTransform))]
    public class TranslateAnimation : TweenAnimationBase
    {
        [Header("Translate")]
        [SerializeField] private Vector3 _offset = Vector3.zero;
        [SerializeField] private bool _from;

        private Vector3 _initialPos;

        private RectTransform Rt => GetComponent<RectTransform>();

        private void Awake()
        {
            _initialPos = Rt.anchoredPosition3D;
        }

        public override void Play()
        {
            DOTween.Kill(Rt);

            if (_from)
            {
                Rt.anchoredPosition = _initialPos + _offset;
                Rt.DOAnchorPos3D(_initialPos, _duration)
                    .SetDelay(_delay)
                    .SetEase(_ease);
            }
            else
            {
                Rt.DOAnchorPos3D(_initialPos + _offset, _duration)
                    .SetDelay(_delay)
                    .SetEase(_ease);
            }
        }

#if UNITY_EDITOR
        public override string GetDescription()
        {
            var text = $"Translate {(_from ? "from" : "by")} {_offset} in {_duration}s";
            if (_delay > 0)
            {
                text += $" delayed {_delay}s";
            }

            return text;
        }
#endif
    }
}