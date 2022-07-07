using System;
using Core.SaveAndLoadEssence;
using PlayerEssence.WeaponEssence;
using UnityEngine;
using Zenject;

namespace WeaponsEssence
{
    [Serializable]
    public class WeaponInventory
    {
        [SerializeField] private WeaponTool[] _weapons;
        
        [Inject] private ISaveAndLoadSystem _saveAndLoad;
        [Inject] private DiContainer _container;

        public WeaponTool GetCurrent()
        {
            WeaponTool tool = _container.InstantiatePrefab(_weapons[_saveAndLoad.GetCurrentWeaponId]).GetComponent<WeaponTool>();
            return tool;
        }
    }
}