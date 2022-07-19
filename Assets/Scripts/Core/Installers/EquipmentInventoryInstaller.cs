using PlayerEssence.ChoosableEquipmentEssence;
using UnityEngine;
using WeaponsEssence;
using Zenject;

namespace Core.Installers
{
    [CreateAssetMenu(fileName = "EquipmentInventoryInstaller", menuName = "Installers/EquipmentInventoryInstaller")]
    public class EquipmentInventoryInstaller : ScriptableObjectInstaller<EquipmentInventoryInstaller>
    {
        [SerializeField] private ChoosableEquipmentInventory _inventory;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ChoosableEquipmentInventory>().FromInstance(_inventory).AsSingle();
            Container.QueueForInject(_inventory);
        }

        [ContextMenu("Reset Saves")]
        private void ResetSaves()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}