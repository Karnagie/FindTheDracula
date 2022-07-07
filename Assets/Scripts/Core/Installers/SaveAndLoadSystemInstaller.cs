using Core.SaveAndLoadEssence;
using WeaponsEssence;
using Zenject;

namespace Core.Installers
{
    public class SaveAndLoadSystemInstaller : MonoInstaller<SaveAndLoadSystemInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ISaveAndLoadSystem>().FromInstance(new PlayerPrefsSaveAndLoadSystem()).AsSingle();
        }
    }
}