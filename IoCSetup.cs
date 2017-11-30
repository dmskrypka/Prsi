using System;
using Unity;
using Unity.Lifetime;
using Prsi.Service.API;
using Prsi.Service.Impl;
using Prsi.Controller.API;
using Prsi.Controller.Impl;
using Prsi.Objects;

namespace Prsi
{
    static class IoCSetup
    {
        private static Lazy<IUnityContainer> containerLazy = new Lazy<IUnityContainer>(() => SetupUnity(), false);

        public static Lazy<IUnityContainer> Container { get { return containerLazy; } }
        private static IUnityContainer SetupUnity()
        {
            IUnityContainer container = new UnityContainer();

            GameConfigImpl config = new GameConfigImpl();

            container.RegisterInstance<IGameConfig>(config, new ContainerControlledLifetimeManager());

            container.RegisterType<MainController, MainControllerImpl>(new ContainerControlledLifetimeManager());
            container.RegisterType<IGameProcess, GameProcessImpl>(new ContainerControlledLifetimeManager());
            container.RegisterType<IGameEventInfo, GameEventInfoImpl>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPlayer, PlayerImpl>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDeck, DeckImpl>(new ContainerControlledLifetimeManager());

            return container;
        }
    }
}
