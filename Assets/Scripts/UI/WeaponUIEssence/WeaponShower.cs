using System;
using Core.SaveAndLoadEssence;
using UnityEngine;
using WeaponsEssence;
using Zenject;

namespace UI.WeaponUIEssence
{
    public class WeaponShower : MonoBehaviour
    {
        [SerializeField] private GameObject[] _weapons;
        [SerializeField] private Transform _parent;
        
        [Inject] private ISaveAndLoadSystem _saveAndLoad;

        private GameObject _current;

        private void Awake()
        {
            _current = Instantiate(_weapons[_saveAndLoad.GetCurrentWeaponId], _parent);
        }

        public void Change(int id)
        {
            Destroy(_current);
            _current = Instantiate(_weapons[id], _parent);
        }
    }
}