﻿using System;
using System.Threading.Tasks;
using AliveEssence;
using Core.BusEvents;
using Core.BusEvents.Handlers;
using PlayerEssence;
using UI;
using UnityEngine;
using UnityEngine.Playables;
using Zenject;

namespace GameEssence
{
    public class Game : MonoBehaviour, IGameStateHandler
    {
        [SerializeField] private PlayableDirector _timeline;
        [SerializeField] private PlayableDirector _toPuzzle;
        [SerializeField] private PlayableDirector _win;

        [SerializeField] private WinPanel _losePanel;

        private Vampire[] _vampires;
        private int _kills;

        [Inject] private Player _player;

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

        public void StartLevel()
        {
            _timeline.Play();
        }

        private async void OnVampireKill()
        {
            _kills++;
            if (_kills == _vampires.Length)
            {
                await Task.Delay(1000);
                await _player.ReturnToPosition();
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

        public void LoseLevel()
        {
            _player.TurnOffControl();
            _losePanel.Show();
        }
    }
}