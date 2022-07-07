using System;
using Core.RaycastingEssence;
using DG.Tweening;
using UnityEngine;

namespace PuzzleEssence.Planks
{
    public class Plank : MonoBehaviour, IRaycastTarget
    {
        [SerializeField] private float _breakSpeed = 0.25f;

        private bool _isBroken;

        public void Break()
        {
            if(_isBroken) return;
            transform.DOScale(0, _breakSpeed);
            _isBroken = true;
        }

        public Action OnStartClick { get; }
        public Action OnEndClick { get; }

        public bool IsBroken => _isBroken;
    }
}