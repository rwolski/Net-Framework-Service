using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Tests
{
    [TestClass]
    public abstract class FrameworkUnitTest
    {
        private static IContainer _globalContainer;
        private static ILifetimeScope _globalScope;

        protected ILifetimeScope Container;
        private TestContext _testContext;

        public FrameworkUnitTest()
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
            var builder = new ContainerBuilder();

            builder.RegisterModule<TestModules>();

            _globalContainer = builder.Build();
            _globalScope = _globalContainer.BeginLifetimeScope();
        }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Container = _globalScope.BeginLifetimeScope();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            _globalScope.Dispose();
            _globalContainer.Dispose();
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            Container.Dispose();
        }
    }
}
