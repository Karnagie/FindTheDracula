using UnityEngine;

namespace Core.SaveAndLoadEssence
{
    public class PlayerPrefsSaveAndLoadSystem : ISaveAndLoadSystem
    {
        public int GetCurrentWeaponId => PlayerPrefs.GetInt("CurrentWeapon", 0);
    }
}