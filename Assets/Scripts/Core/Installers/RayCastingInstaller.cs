using System;
using Core.InputEssence;
using Core.RayCastingEssence;
using Zenject;

namespace Core.Installers
{
    public class RayCastingInstaller : MonoInstaller
    {
        private RayCasting _rayCasting;

        [Inject] private IInputSystem _inputSystem;
        
        public override void InstallBindings()
        {
            _rayCasting = new RayCasting(_inputSystem);
            Container.Bind<RayCasting>().FromInstance(_rayCasting).AsSingle();
        }

        private void OnDisable()
        {
            _rayCasting.Dispose();
        }
    }
}