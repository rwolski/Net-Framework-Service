using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebApp.API.Tests
{
    [TestClass]
    public abstract class UnitTest
    {
        protected IContainer _globalContainer;
        protected ILifetimeScope Container;

        //private ContainerBuilder _containerBuilder;
        private TestContext _testContext;

        public UnitTest()
        {
        }

        public TestContext TestContext
        {
            get { return _testContext; }
            set { _testContext = value; }
        }

        [AssemblyInitialize()]
        public static void AssemblyInitialise(TestContext context)
        {
        }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            var builder = new ContainerBuilder();
            RegisterTypes(builder);

            _globalContainer = builder.Build();

            Container = _globalContainer.BeginLifetimeScope();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            Container.Dispose();
            _globalContainer.Dispose();
            //_containerBuilder = null;
        }

        protected virtual void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterModule<TestModules>();
        }

        protected virtual void UpdateContainer(ContainerBuilder builder = null)
        {
            builder = builder ?? new ContainerBuilder();
            builder.Update(_globalContainer);
        }
    }
}