using UnityEngine;
using WeaponsEssence;
using Zenject;

namespace UI.WeaponUIEssence
{
    public class WeaponShower : MonoBehaviour
    {
        [Inject] private WeaponInventory _inventory;
    }
}