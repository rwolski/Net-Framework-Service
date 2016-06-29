using Framework.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using WebApp.API.Controllers;
using Autofac.Integration.WebApi;
using System.Web.Http;
using Framework.Queue;
using Framework.Cache;
using WebApp.API.Models;
using System;

namespace WebApp.API.Tests.Controllers
{
    public class PowerballControllerTest : FrameworkUnitTest
    {
        HttpConfiguration _httpConfig;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();

            _httpConfig = new HttpConfiguration()
            {
                DependencyResolver = new AutofacWebApiDependencyResolver(Container)
            };
        }

        [TestCleanup]
        public override void TestCleanup()
        {
            base.TestCleanup();

            _httpConfig.Dispose();
        }

        [TestMethod]
        public void PowerballCacheGet()
        {
            // Arrange
            PowerballController controller = new PowerballController()
            {
                Configuration = _httpConfig
            };

            var queue = new Moq.Mock<ICacheStore>();
            queue.Setup(x => x.GetObject<PowerballDrawModel>()).Returns(new PowerballDrawModel()
            {
                DrawNumber = 1,
                DrawDateTime = DateTime.Now,
                DrawStatus = DrawStatusCode.Closed,
                DrawWinningNumbers = new List<int>() { 2, 3, 4 }
            });

            var result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.DrawNumber);
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }
    }
}
