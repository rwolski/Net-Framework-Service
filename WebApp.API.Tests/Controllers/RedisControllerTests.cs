using Autofac.Extras.Moq;
using Framework.Cache;
using Framework.Queue;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.API.Controllers;
using WebApp.API.Contracts;

namespace WebApp.API.Tests.Controllers
{
    [TestClass]
    public class RedisControllerTest : ControllerUnitTest
    {
        [TestMethod]
        public async Task QueueCacheGet_TestCache()
        {
            using (var mock = AutoMock.GetLoose())
            {
                DateTime drawTime = DateTime.Now;

                // The cached results
                var cache = mock.Mock<ICacheStore>();
                cache.Setup(x => x.GetObject<RedisTestModel>()).Returns(Task.FromResult(new RedisTestModel()
                {
                    Number = 1,
                    DateTime = drawTime,
                    Status = RedisStatusCode.Closed,
                    Numbers = new List<int>() { 2, 3, 4 }
                }));
                mock.Mock<ICacheProvider>()
                    .Setup(x => x.GetStore(It.IsAny<int>())).Returns(cache.Object);

                // The queued results - shouldn't reach this
                var queue = mock.Mock<ISimpleQueue<RedisTestModel>>();
                queue.Setup(x => x.Receive()).Returns(Task.FromResult(new RedisTestModel()
                {
                    Number = 2,
                    DateTime = drawTime,
                    Status = RedisStatusCode.Open,
                    Numbers = new List<int>()
                }));
                mock.Mock<ISimpleQueueProvider>()
                    .Setup(x => x.GetQueue<RedisTestModel>()).Returns(queue.Object);

                var controller = mock.Create<RedisTestController>();
                var result = await controller.Get();

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.Number);
                Assert.AreEqual(drawTime, result.DateTime);
                Assert.AreEqual(DrawStatusCode.Closed, result.Status);
                Assert.AreEqual(2, result.Numbers.ElementAt(0));
                Assert.AreEqual(3, result.Numbers.ElementAt(1));
                Assert.AreEqual(4, result.Numbers.ElementAt(2));
            }
        }

        [TestMethod]
        public async Task QueueCacheGet_TestQueue()
        {
            using (var mock = AutoMock.GetLoose())
            {
                DateTime drawTime = DateTime.Now;

                var cache = mock.Mock<ICacheStore>();
                cache.Setup(x => x.GetObject<RedisTestModel>()).Returns(Task.FromResult((RedisTestModel)null));
                mock.Mock<ICacheProvider>()
                    .Setup(x => x.GetStore(It.IsAny<int>())).Returns(cache.Object);

                // Should return the queued results now
                var queue = mock.Mock<ISimpleQueue<RedisTestModel>>();
                queue.Setup(x => x.Receive()).Returns(Task.FromResult(new RedisTestModel()
                {
                    Number = 2,
                    DateTime = drawTime,
                    Status = RedisStatusCode.Open,
                    Numbers = new List<int>()
                }));
                mock.Mock<ISimpleQueueProvider>()
                    .Setup(x => x.GetQueue<RedisTestModel>()).Returns(queue.Object);

                var controller = mock.Create<RedisTestController>();
                var result = await controller.Get();

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(2, result.Number);
                Assert.AreEqual(drawTime, result.DateTime);
                Assert.AreEqual(DrawStatusCode.Open, result.Status);
                Assert.AreEqual(0, result.Numbers.Count());
            }
        }
    }
}
