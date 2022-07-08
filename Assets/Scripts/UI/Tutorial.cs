using System;
using Core.SaveAndLoadEssence;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class Tutorial : MonoBehaviour
    {
        [Inject] private ISaveAndLoadSystem _saveAndLoad;

        [SerializeField] private Button[] _buttons;
        
        private void Awake()
        {
            if(!_saveAndLoad.Tutorial)
                gameObject.SetActive(false);

            foreach (var button in _buttons)
            {
                button.onClick.AddListener(OnClick);
            }
        }

        private void OnClick()
        {
            gameObject.SetActive(false);
        }
    }
}