using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.SaveAndLoadEssence
{
    public class PlayerPrefsSaveAndLoadSystem : ISaveAndLoadSystem
    {
        public int GetCurrentWeaponId => PlayerPrefs.GetInt("CurrentWeapon", 0);
        public int GetCurrentEquipmentId => PlayerPrefs.GetInt("CurrentEquipment", 0);
        public int NextLevel => CalculateNextLevel();
        public bool Tutorial => GetTutorial();
        public int CurrentLevel => SceneManager.GetActiveScene().buildIndex;

        private bool GetTutorial()
        {
            if (PlayerPrefs.GetInt("Tutorial", 0) == 0)
            {
                PlayerPrefs.SetInt("Tutorial", 1);
                return true;
            }

            return false;
        }

        private int _openingId;

        private int CalculateNextLevel()
        {
            int index = SceneManager.GetActiveScene().buildIndex;//PlayerPrefs.GetInt("NextLevel", 0);
            index = (index + 1) % 4;
            // if (index == SceneManager.GetActiveScene().buildIndex)
            // {
            //     Debug.Log(index);
            //     index = (index + 1) % 4;
            // }

            return index;
        }

        public int IsNextWeaponOrQuestion()
        {
            int r = PlayerPrefs.GetInt("WeaponOrEquipment", -1);
            
            if (r == -1)
                r = Random.Range(0, 2);
            
            PlayerPrefs.SetInt("WeaponOrEquipment", r);
            return r;
        }
        
        

        public void ChangeCurrentWeapon(int id)
        {
            PlayerPrefs.SetInt("CurrentWeapon", id);
        }

        public bool IsOpenedWeapon(int index)
        {
            return PlayerPrefs.GetInt($"Weapon_{index}", -1) != -1;
        }

        public void OpenWeapon(int index)
        {
            PlayerPrefs.SetInt($"Weapon_{index}", 1);
        }

        public void AddOpenWeaponPercents(int index, float value)
        {
            float newValue = PlayerPrefs.GetFloat($"Weapon_{index}_opening", 0) + value;
            Debug.Log($"Weapon_{index}_opening {newValue}");
            PlayerPrefs.SetFloat($"Weapon_{index}_opening", newValue);
            PlayerPrefs.SetInt($"Weapon_CurrentOpening", index);
            _openingId = index;
            
            if (newValue >= 1)
            {
                int opening = GetCurrentWeaponOpening();
                List<int> free = new List<int>();
                if (opening == -1 || IsOpenedWeapon(opening))
                {
                    int i = 0;
                    while (i < 3)
                    {
                        if(!IsOpenedWeapon(i))
                            free.Add(i);
                        i++;
                    }
                }

                PlayerPrefs.SetInt($"Weapon_{index}", 1);
                
                int r = Random.Range(0, 2);
                if (free.Count >= 1)
                {
                    PlayerPrefs.SetInt("WeaponOrEquipment", r);
                }
                else
                {
                    r = 1;
                    PlayerPrefs.SetInt("WeaponOrEquipment", r);
                }
            }
        }

        public int GetCurrentWeaponOpening()
        {
            return PlayerPrefs.GetInt($"Weapon_CurrentOpening", -1);
        }

        public float FillWeaponPercent()
        {
            return PlayerPrefs.GetFloat($"Weapon_{_openingId}_opening", 0);
        }
        
        //
        //
        //
        
        public void ChangeCurrentEquipment(int id)
        {
            PlayerPrefs.SetInt("CurrentEquipment", id);
        }

        public bool IsOpenedEquipment(int index)
        {
            return PlayerPrefs.GetInt($"Equipment_{index}", -1) != -1;
        }

        public void OpenEquipment(int index)
        {
            PlayerPrefs.SetInt($"Equipment_{index}", 1);
        }

        public void AddOpenEquipmentPercents(int index, float value)
        {
            float newValue = PlayerPrefs.GetFloat($"Equipment_{index}_opening", 0) + value;
            Debug.Log($"Equipment_{index}_opening {newValue}");
            PlayerPrefs.SetFloat($"Equipment_{index}_opening", newValue);
            PlayerPrefs.SetInt($"Equipment_CurrentOpening", index);
            _openingId = index;
            
            if (newValue >= 1)
            {
                int opening = GetCurrentEquipmentOpening();
                List<int> free = new List<int>();
                if (opening == -1 || IsOpenedWeapon(opening))
                {
                    int i = 0;
                    while (i < 3)
                    {
                        if(!IsOpenedWeapon(i))
                            free.Add(i);
                        i++;
                    }
                }

                PlayerPrefs.SetInt($"Equipment_{index}", 1);
                
                int r = Random.Range(0, 2);
                if (free.Count >= 1)
                {
                    PlayerPrefs.SetInt("WeaponOrEquipment", r);
                }
                else
                {
                    r = 0;
                    PlayerPrefs.SetInt("WeaponOrEquipment", r);
                }
            }
        }

        public int GetCurrentEquipmentOpening()
        {
            return PlayerPrefs.GetInt($"Equipment_CurrentOpening", -1);
        }

        public float FillEquipmentPercent()
        {
            return PlayerPrefs.GetFloat($"Equipment_{_openingId}_opening", 0);
        }
    }
}