using System;
using Core.SaveAndLoadEssence;
using GameEssence;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class Tutorial : MonoBehaviour
    {
        [Inject] private ISaveAndLoadSystem _saveAndLoad;
        
        [SerializeField] private Game _game;
        [SerializeField] private Button[] _buttons;
        [SerializeField] private Tutorial _next;
        [SerializeField] private bool _onAwake;
        
        private void Awake()
        {
            if(_onAwake == false)
            {
                _game.VampireKill += OnVampireKill;
                gameObject.SetActive(false);
                return;
            }

            if(!_saveAndLoad.Tutorial)
                gameObject.SetActive(false);

            foreach (var button in _buttons)
            {
                button.onClick.AddListener(OnClick);
            }
        }

        private void OnVampireKill()
        {
            gameObject.SetActive(false);
        }

        private void OnClick()
        {
            gameObject.SetActive(false);
            if (_next != null)
                _next.Show();
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }
    }
}