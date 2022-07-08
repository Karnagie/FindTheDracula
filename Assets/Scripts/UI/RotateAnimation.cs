using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class RotateAnimation : MonoBehaviour
    {
        [SerializeField] private float _speed = 1;
        
        private void Awake()
        {
            transform.DOLocalRotate(Vector3.forward * 360, _speed, RotateMode.LocalAxisAdd)
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        }
    }
}