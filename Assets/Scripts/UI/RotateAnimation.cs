using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class RotateAnimation : MonoBehaviour
    {
        [SerializeField] private float _speed = 1;

        private bool _enabled;
        
        private void OnEnable()
        {
            if(_enabled) return;
            transform.DOLocalRotate(Vector3.forward * 360, _speed, RotateMode.LocalAxisAdd)
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        }
    }
}