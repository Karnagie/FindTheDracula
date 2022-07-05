using System;
using Core.BusEvents;
using Core.BusEvents.Handlers;
using Core.InputEssence;
using Core.RayCastingEssence;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace PuzzleEssence.Planks
{
    public class PlanksPuzzle : Puzzle
    {
        [SerializeField] private Plank[] _planks;

        [Inject] private IInputSystem _input;
        [Inject] private RayCasting _rayCasting;

        private void Awake()
        {
            _input.OnStartClick += OnClick;
            OnSolved += () => EventBus.RaiseEvent((IGameStateHandler handler) => handler.FinishLevel());
        }

        private void OnClick(InputAction.CallbackContext obj)
        {
            _rayCasting.Cast<Plank>()?.Break();
            CheckProgress();
        }

        private void CheckProgress()
        {
            foreach (var plank in _planks)
            {
                if(!plank.IsBroken) return;
            }
            Solve();
        }
    }
}