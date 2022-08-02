using System;
using Core.SaveAndLoadEssence;
using UnityEngine;
using Zenject;

namespace UI
{
    public class NewUnlockReminder : MonoBehaviour
    {
        [SerializeField] private bool _tool;
        
        [Inject] private ISaveAndLoadSystem _saveAndLoad;
        
        private void Awake()
        {
            if (_saveAndLoad.OpenedNewTool == false && _tool)
            {
                gameObject.SetActive(false);
            }else if (_saveAndLoad.OpenedNewWeapon == false && !_tool)
            {
                gameObject.SetActive(false);
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            if(_tool)_saveAndLoad.ResetOpenedNewTool();
            else _saveAndLoad.ResetOpenedWeapon();
        }
    }
}