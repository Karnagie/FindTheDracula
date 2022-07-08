using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.SaveAndLoadEssence
{
    public class PlayerPrefsSaveAndLoadSystem : ISaveAndLoadSystem
    {
        public int GetCurrentWeaponId => PlayerPrefs.GetInt("CurrentWeapon", 0);
        public int NextLevel => CalculateNextLevel();
        public bool Tutorial => GetTutorial();

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
            _openingId = index;
            
            if (newValue >= 1) PlayerPrefs.SetInt($"Weapon_{index}", 1);
        }

        public int GetCurrentOpening()
        {
            return PlayerPrefs.GetInt($"Weapon_CurrentOpening", -1);
        }

        public float FillPercent()
        {
            return PlayerPrefs.GetFloat($"Weapon_{_openingId}_opening", 0);
        }
    }
}