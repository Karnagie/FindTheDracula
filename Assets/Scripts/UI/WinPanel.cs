﻿using System;
using System.Threading.Tasks;
using UI.SequenceEssence;
using UnityEngine;

namespace UI
{
    public class WinPanel : MonoBehaviour
    {
        [SerializeField] private AnimationSequence _sequence;
        [SerializeField] private CanvasGroup _canvas;
        [SerializeField] private float _fadeSpeed = 10;

        private void Awake()
        {
            _canvas.alpha = 0;
        }

        public async void Show()
        {
            _canvas.alpha = 1;
            _sequence.Play();
        }
    }
}