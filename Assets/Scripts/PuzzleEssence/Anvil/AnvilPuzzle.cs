using Core.BusEvents;
using Core.BusEvents.Handlers;
using Core.InputEssence;
using Core.RayCastingEssence;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace PuzzleEssence.Anvil
{
    public class AnvilPuzzle : Puzzle
    {
        [SerializeField] private Anvil _anvil;
        
        [Inject] private IInputSystem _input;
        [Inject] private RayCasting _rayCasting;

        private void Awake()
        {
            _input.OnStartClick += OnClick;
            OnSolved += () => EventBus.RaiseEvent((IGameStateHandler handler) => handler.FinishLevel());
            _anvil.OnAnimationEnd += Solve;
        }

        private void OnClick(InputAction.CallbackContext obj)
        {
            _rayCasting.Cast<Anvil>()?.Open();
        }
    }
}