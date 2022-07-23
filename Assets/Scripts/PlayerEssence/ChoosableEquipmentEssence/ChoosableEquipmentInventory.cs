using System;
using System.Collections.Generic;
using Core.SaveAndLoadEssence;
using PlayerEssence.ToolsEssence;
using PlayerEssence.WeaponEssence;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace PlayerEssence.ChoosableEquipmentEssence
{
    [Serializable]
    public class ChoosableEquipmentInventory
    {
        [SerializeField] private Tool[] _tools;
        
        [Inject] private ISaveAndLoadSystem _saveAndLoad;
        [Inject] private DiContainer _container;

        public Tool GetCurrent()
        {
            Tool tool = _container.InstantiatePrefab(_tools[_saveAndLoad.GetCurrentEquipmentId]).GetComponent<Tool>();
            return tool;
        }

        public void AddPercents(float count)
        {
            int opening = _saveAndLoad.GetCurrentEquipmentOpening();
            if (opening == -1 || _saveAndLoad.IsOpenedEquipment(opening))
            {
                List<int> free = new List<int>();
                int i = 0;
                foreach (var index in _tools)
                {
                    if(i == 0)
                    {
                        i++;
                        continue;
                    }
                    if(!_saveAndLoad.IsOpenedEquipment(i))
                    {
                        free.Add(i);
                        Debug.Log(i);
                    }
                    i++;
                }
                opening = free[Random.Range(0, free.Count)];
            }
            if (opening == -1 || _saveAndLoad.IsOpenedEquipment(opening)) opening = 0;
            
            _saveAndLoad.AddOpenEquipmentPercents(opening, count);
        }

        public Tool Get(int getCurrentOpening)
        {
            Tool tool = _container.InstantiatePrefab(_tools[getCurrentOpening]).GetComponent<Tool>();
            return tool;
        }

        public bool IsAllFilled()
        {
            List<int> free = new List<int>();
            int i = 0;
            foreach (var index in _tools)
            {
                if(!_saveAndLoad.IsOpenedEquipment(i))
                    free.Add(i);
                i++;
            }

            if (free.Count == 0) return true;
            return false;
        }
    }
}