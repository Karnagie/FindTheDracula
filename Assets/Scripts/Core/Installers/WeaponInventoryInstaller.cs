using Core.SaveAndLoadEssence;
using UnityEngine;
using WeaponsEssence;
using Zenject;

namespace Core.Installers
{
    [CreateAssetMenu(fileName = "WeaponInventoryInstaller", menuName = "Installers/WeaponInventoryInstaller")]
    public class WeaponInventoryInstaller : ScriptableObjectInstaller<WeaponInventoryInstaller>
    {
        [SerializeField] private WeaponInventory _inventory;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<WeaponInventory>().FromInstance(_inventory).AsSingle();
            Container.QueueForInject(_inventory);
        }

        [ContextMenu("Reset Saves")]
        private void ResetSaves()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}