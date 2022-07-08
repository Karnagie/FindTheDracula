using UnityEngine;

namespace Core.SaveAndLoadEssence
{
    public class PlayerPrefsSaveAndLoadSystem : ISaveAndLoadSystem
    {
        public int GetCurrentWeaponId => PlayerPrefs.GetInt("CurrentWeapon", 0);
        public void ChangeCurrentWeapon(int id)
        {
            PlayerPrefs.SetInt("CurrentWeapon", id);
        }

        public bool IsOpenedWeapon(int index)
        {
            return true;//PlayerPrefs.GetInt($"Weapon_{index}");
        }
    }
}