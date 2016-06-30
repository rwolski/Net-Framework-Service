using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http;

namespace WebApp.API.Tests
{
    [TestClass]
    public abstract class ControllerUnitTest : UnitTest
    {
        protected HttpConfiguration HttpConfig;

        public ControllerUnitTest()
        {
        }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();

            BuildHttpConfiguration();
        }

        [TestCleanup]
        public override void TestCleanup()
        {
            HttpConfig.Dispose();

            base.TestCleanup();
        }

        protected virtual HttpConfiguration BuildHttpConfiguration()
        {
            HttpConfig = new HttpConfiguration()
            {
                DependencyResolver = new Autofac.Integration.WebApi.AutofacWebApiDependencyResolver(Container)
            };

            return HttpConfig;
        }

        protected override void UpdateContainer(ContainerBuilder builder = null)
        {
            base.UpdateContainer(builder);

            BuildHttpConfiguration();
        }
    }
}

