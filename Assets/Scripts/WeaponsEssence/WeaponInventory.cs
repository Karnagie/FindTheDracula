using System;
using System.Collections.Generic;
using Core.SaveAndLoadEssence;
using PlayerEssence.WeaponEssence;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

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

        public void AddPercents(float count)
        {
            int opening = _saveAndLoad.GetCurrentOpening();
            if (opening == -1 || _saveAndLoad.IsOpenedWeapon(opening))
            {
                List<int> free = new List<int>();
                int i = 0;
                foreach (var index in _weapons)
                {
                    if(!_saveAndLoad.IsOpenedWeapon(i))
                        free.Add(i);
                    i++;
                }
                opening = free[Random.Range(0, free.Count)];
            }
            if (opening == -1 || _saveAndLoad.IsOpenedWeapon(opening)) opening = 0;
            
            _saveAndLoad.AddOpenPercents(opening, count);
        }

        public WeaponTool Get(int getCurrentOpening)
        {
            WeaponTool tool = _container.InstantiatePrefab(_weapons[getCurrentOpening]).GetComponent<WeaponTool>();
            return tool;
        }
    }
}