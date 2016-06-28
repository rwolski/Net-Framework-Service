using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Tests
{
    public abstract class FrameworkUnitTest
    {
        private static IContainer _globalContainer;
        protected static ILifetimeScope Container;

        public FrameworkUnitTest()
        {
        }

        [AssemblyInitialize()]
        public static void AssemblyInitialise(TestContext context)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<TestModules>();

            _globalContainer = builder.Build();

            Container = _globalContainer.BeginLifetimeScope();
        }

        [ClassInitialize()]
        public static void ClassInitialise(TestContext context)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<TestModules>();

            _globalContainer = builder.Build();

            Container = _globalContainer.BeginLifetimeScope();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            Container.Dispose();
        }
    }
}
