using Core.RayCastingEssence;
using Zenject;

namespace Core.Installers
{
    public class RayCastingInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<RayCasting>().FromNew().AsSingle();
        }
    }
}