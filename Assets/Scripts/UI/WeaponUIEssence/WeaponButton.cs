using System;
using Core.SaveAndLoadEssence;
using UnityEngine;
using Zenject;

namespace UI.WeaponUIEssence
{
    public class WeaponButton : MonoBehaviour
    {
        [SerializeField] private int _index;

        [SerializeField] private GameObject _lock;
        [SerializeField] private GameObject _opened;

        [Inject] private ISaveAndLoadSystem _saveAndLoad;

        private void Awake()
        {
            if (_saveAndLoad.IsOpenedWeapon(_index))
                Open();
            else
                Lock();
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