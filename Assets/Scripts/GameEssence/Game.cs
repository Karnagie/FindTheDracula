using System;
using System.Threading.Tasks;
using Core.BusEvents;
using Core.BusEvents.Handlers;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using VampireEssence;

namespace GameEssence
{
    public class Game : MonoBehaviour, IGameStateHandler
    {
        [SerializeField] private PlayableDirector _timeline;
        [SerializeField] private PlayableDirector _toPuzzle;
        [SerializeField] private PlayableDirector _win;
        
        private Vampire[] _vampires;
        private int _kills;

        private void Awake()
        {
            EventBus.Subscribe(this);
        }

        private void Start()
        {
            _vampires = FindObjectsOfType<Vampire>();
            foreach (var vampire in _vampires)
            {
                vampire.OnKill += OnVampireKill;
            }
        }

        private async void OnVampireKill()
        {
            _kills++;
            if (_kills == _vampires.Length)
            {
                await Task.Delay(2000);
                GoToPuzzle();
            }
        }

        public void GoToPuzzle()
        {
            _timeline.Stop();
            _toPuzzle.Play();
        }

        public void FinishLevel()
        {
            _toPuzzle.Stop(); 
            _win.Play();
        }
    }
}