using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnackersUI
{
    public class TweenAnimatedUIElement : MonoBehaviour
    {
        [Tooltip("Trigger Show/Hide methods for children TweenAnimatedUIElement")]
        [SerializeField] private bool _propagateToChildren;
        [SerializeField] private bool _disableOnAwake;
        [SerializeField, Min(0)] private float _hideDelay = 0.3f;
        [SerializeField, HideInInspector] private List<TweenAnimationBase> _inAnimations;
        [SerializeField, HideInInspector] private List<TweenAnimationBase> _outAnimations;

        private void Awake()
        {
            if (_disableOnAwake)
                gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            foreach (var anim in _inAnimations)
                if (anim != null)
                    anim.Play();

            if (_propagateToChildren)
                IterateChildrenElements(element => element.Show());
        }

        public void Hide()
        {
            foreach (var anim in _outAnimations)
                if (anim != null)
                    anim.Play();

            if (_hideDelay <= 0)
                gameObject.SetActive(false);
            else if (gameObject.activeInHierarchy)
                StartCoroutine(Co_DelayedHide(_hideDelay));

            if (_propagateToChildren)
                IterateChildrenElements(element => element.Hide());
        }

        private void IterateChildrenElements(Action<TweenAnimatedUIElement> iterator)
        {
            var children = GetComponentsInChildren<TweenAnimatedUIElement>(true);
            foreach (var element in children)
                if (element.transform != transform)
                    iterator(element);
        }

        private IEnumerator Co_DelayedHide(float delay)
        {
            yield return new WaitForSeconds(delay);
            gameObject.SetActive(false);
        }
    }
}