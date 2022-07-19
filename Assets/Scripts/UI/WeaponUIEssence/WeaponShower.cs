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
        [SerializeField] private bool _isEquipment;
        
        [Inject] private ISaveAndLoadSystem _saveAndLoad;

        private GameObject _current;

        private void Awake()
        {
            if(!_isEquipment)_current = Instantiate(_weapons[_saveAndLoad.GetCurrentWeaponId], _parent);
            else _current = Instantiate(_weapons[_saveAndLoad.GetCurrentEquipmentId], _parent);
        }

        public void Change(int id)
        {
            if (!_isEquipment)
            {
                Destroy(_current);
                _current = Instantiate(_weapons[id], _parent);
                _saveAndLoad.ChangeCurrentWeapon(id);
            }
            else
            {
                Destroy(_current);
                _current = Instantiate(_weapons[id], _parent);
                _saveAndLoad.ChangeCurrentEquipment(id);
            }
        }
    }
}