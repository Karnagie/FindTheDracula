using System;
using Core.SaveAndLoadEssence;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.WeaponUIEssence
{
    public class WeaponButton : MonoBehaviour
    {
        [SerializeField] private int _index;
        [SerializeField] private bool _openedByDefault;
        [SerializeField] private bool _isEquipment;

        [SerializeField] private GameObject _lock;
        [SerializeField] private GameObject _opened;
        [SerializeField] private Button _button;
        [SerializeField] private WeaponShower _shower;

        [Inject] private ISaveAndLoadSystem _saveAndLoad;

        private void Awake()
        {
            if (_isEquipment)
            {
                if (_openedByDefault) _saveAndLoad.OpenEquipment(_index);
            
                if (_saveAndLoad.IsOpenedEquipment(_index))
                    Open();
                else
                    Lock();
            }
            else
            {
                if (_openedByDefault) _saveAndLoad.OpenWeapon(_index);
            
                if (_saveAndLoad.IsOpenedWeapon(_index))
                    Open();
                else
                    Lock();   
            }

            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if (_isEquipment)
            {
                if (_saveAndLoad.IsOpenedEquipment(_index))
                {
                    _shower.Change(_index);
                }
            }
            else
            {
                if (_saveAndLoad.IsOpenedWeapon(_index))
                {
                    _shower.Change(_index);
                }
            }
        }

        public void Lock()
        {
            _lock.SetActive(true);
            _opened.SetActive(false);
        }

        public void Open()
        {
            _lock.SetActive(false);
            _opened.SetActive(true);
        }
    }
}