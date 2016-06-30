using Autofac;
using Autofac.Extras.Moq;
using Framework.Cache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WebApp.API.Controllers;
using WebApp.API.Models;

namespace WebApp.API.Tests.Controllers
{
    [TestClass]
    public class PowerballControllerTest : ControllerUnitTest
    {
        [TestMethod]
        public void PowerballCacheGet()
        {
            DateTime drawTime = DateTime.Now;

            using (var mock = AutoMock.GetLoose())
            {
                var queue = mock.Mock<ICacheStore>();
                queue.Setup(x => x.GetObject<PowerballDrawModel>()).Returns(new PowerballDrawModel()
                {
                    DrawNumber = 1,
                    DrawDateTime = DateTime.Now,
                    DrawStatus = DrawStatusCode.Closed,
                    DrawWinningNumbers = new List<int>() { 2, 3, 4 }
                });

                var builder = new ContainerBuilder();
                builder.Register(c => queue.Object).As<ICacheStore>().InstancePerLifetimeScope();
                UpdateContainer(builder);

                // Arrange
                var controller = Container.Resolve<PowerballController>();
                controller.Configuration = HttpConfig;

                var result = controller.Get();

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.DrawNumber);
                Assert.AreEqual(drawTime, result.DrawDateTime);
                Assert.AreEqual(DrawStatusCode.Closed, result.DrawStatus);
                Assert.AreEqual(new List<int>() { 1, 2, 3, 4 }, result.DrawWinningNumbers);
            }
        }
    }
}
