using System;
using Unity;
using Unity.Lifetime;


namespace Prsi
{
    static class IoCSetup
    {
        private static Lazy<IUnityContainer> containerLazy = new Lazy<IUnityContainer>(() => SetupUnity(), false);

        public static Lazy<IUnityContainer> Container { get { return containerLazy; } }
        private static IUnityContainer SetupUnity()
        {
            IUnityContainer container = new UnityContainer();

            ConfigCoreServiceImpl config = new ConfigCoreServiceImpl();

            container.RegisterInstance<IConfigurationCoreService>(config, new ContainerControlledLifetimeManager());

            container.RegisterType<MainController, ControllerImpl>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDbMessageManager, DbMessageManagerImpl>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISmsServer, SmsServerImpl>(new ContainerControlledLifetimeManager());

            return container;
        }
    }
}
