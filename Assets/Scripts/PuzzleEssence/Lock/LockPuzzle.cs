using Core.BusEvents;
using Core.BusEvents.Handlers;
using Core.InputEssence;
using Core.RayCastingEssence;
using PuzzleEssence.Planks;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace PuzzleEssence.Lock
{
    public class LockPuzzle : Puzzle
    {
        [SerializeField] private Lockpick _lockpick;
        
        [Inject] private IInputSystem _input;
        [Inject] private RayCasting _rayCasting;

        private void Awake()
        {
            _input.OnStartClick += OnClick;
            OnSolved += () => EventBus.RaiseEvent((IGameStateHandler handler) => handler.FinishLevel());
            _lockpick.OnAnimationEnd += Solve;
        }

        private void OnClick(InputAction.CallbackContext obj)
        {
            _rayCasting.Cast<Lockpick>()?.Open();
        }
    }
}