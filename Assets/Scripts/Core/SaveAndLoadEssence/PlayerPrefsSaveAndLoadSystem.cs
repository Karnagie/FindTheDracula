using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.SaveAndLoadEssence
{
    public class PlayerPrefsSaveAndLoadSystem : ISaveAndLoadSystem
    {
        public int GetCurrentWeaponId => PlayerPrefs.GetInt("CurrentWeapon", 0);
        public int NextLevel => CalculateNextLevel();

        private int CalculateNextLevel()
        {
            int index = PlayerPrefs.GetInt("NextLevel", 1);
            if (index == SceneManager.GetActiveScene().buildIndex)
            {
                index = (index + 1) % 2;
            }

            return index;
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

        public void AddOpenPercents(int index, float value)
        {
            float newValue = PlayerPrefs.GetFloat($"Weapon_{index}_opening", 0) + value;
            Debug.Log($"Weapon_{index}_opening {newValue}");
            PlayerPrefs.SetFloat($"Weapon_{index}_opening", newValue);
            PlayerPrefs.SetInt($"Weapon_CurrentOpening", index);
            
            if (newValue >= 1) PlayerPrefs.SetInt($"Weapon_{index}", 1);
        }

        public int GetCurrentOpening()
        {
            return PlayerPrefs.GetInt($"Weapon_CurrentOpening", -1);
        }

        public float FillPercent()
        {
            return PlayerPrefs.GetFloat($"{GetCurrentWeaponId}", 0);
        }
    }
}