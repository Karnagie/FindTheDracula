using System;
using Core.InputEssence;
using Zenject;

namespace Core.Installers
{
    public class InputSystemInstaller : MonoInstaller
    {
        private IInputSystem _inputSystem;
        
        public override void InstallBindings()
        {
            _inputSystem = new PcInputSystem();
            Container.Bind<IInputSystem>().FromInstance(_inputSystem).AsSingle();
        }

        private void OnDisable()
        {
            _inputSystem.Dispose();
        }
    }
}