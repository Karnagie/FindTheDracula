using Core.InputEssence;
using Zenject;

namespace Core.Installers
{
    public class InputSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputSystem>().FromInstance(new PcInputSystem()).AsSingle();
        }
    }
}