using System;
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
        [SerializeField] private int[] _targetsOnRoom;
        
        [SerializeField] private PlayableDirector _timeline;
        [SerializeField] private PlayableDirector[] _rooms;
        [SerializeField] private PlayableDirector _toPuzzle;
        [SerializeField] private PlayableDirector _win;

        [SerializeField] private WinPanel _losePanel;
        [SerializeField] private FillImage _fill;

        private Vampire[] _vampires;
        private int _kills;
        private int _currentRoom;

        [Inject] private Player _player;

        public event Action VampireKill;

        private void Awake()
        {
            EventBus.Subscribe(this);
            _fill.Init();
        }

        private void Start()
        {
            _vampires = FindObjectsOfType<Vampire>();
            foreach (var vampire in _vampires)
            {
                vampire.OnKill += OnVampireKill;
            }
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(this);
            EventBus.Clear();
        }

        public void StartLevel()
        {
            _timeline.Play();
        }

        private async void OnVampireKill()
        {
            _kills++;
            VampireKill?.Invoke();
            if (_kills == _targetsOnRoom[_currentRoom] && _kills != _vampires.Length)
            {
                await Task.Delay(2000);
                _fill.Animate();
                await _player.ReturnToPosition();
                GoToRoom(_currentRoom);
                _currentRoom++;
            }
            if (_kills == _vampires.Length)
            {
                Debug.Log("To wait");
                await Task.Delay(2000);
                _fill.Animate();
                Debug.Log("To return");
                await _player.ReturnToPosition();
                GoToPuzzle();
            }
        }
        
        public void GoToRoom(int index)
        {
            Debug.Log($"To room {index}");
            _rooms[index].Play();
        }

        public void GoToPuzzle()
        {
            _timeline.Stop();
            Debug.Log("To puzzle");
            _toPuzzle.Play();
        }

        public void FinishLevel()
        {
            _toPuzzle.Stop(); 
            _win.Play();
            _fill.Animate();
        }

        public void LoseLevel()
        {
            _player.TurnOffControl();
            _player.TurnOffUI();
            _losePanel.Show();
        }
    }
}