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
using WebApp.API.Models;
using WebApp.API.Contracts;

namespace WebApp.API.Tests.Controllers
{
    [TestClass]
    public class PowerballControllerTest : ControllerUnitTest
    {
        [TestMethod]
        public async Task QueueCacheGet_TestCache()
        {
            using (var mock = AutoMock.GetLoose())
            {
                DateTime drawTime = DateTime.Now;

                // The cached results
                var cache = mock.Mock<ICacheStore>();
                cache.Setup(x => x.GetObject<RabbitLottoDrawModel>()).Returns(Task.FromResult(new RabbitLottoDrawModel()
                {
                    DrawNumber = 1,
                    DrawDateTime = drawTime,
                    DrawStatus = DrawStatusCode.Closed,
                    DrawWinningNumbers = new List<int>() { 2, 3, 4 }
                }));
                mock.Mock<ICacheProvider>()
                    .Setup(x => x.GetStore(It.IsAny<int>())).Returns(cache.Object);

                // The queued results - shouldn't reach this
                var queue = mock.Mock<ISimpleQueue<RabbitLottoDrawModel>>();
                queue.Setup(x => x.Receive()).Returns(Task.FromResult(new RabbitLottoDrawModel()
                {
                    DrawNumber = 2,
                    DrawDateTime = drawTime,
                    DrawStatus = DrawStatusCode.Open,
                    DrawWinningNumbers = new List<int>()
                }));
                mock.Mock<ISimpleQueueProvider>()
                    .Setup(x => x.GetQueue<RabbitLottoDrawModel>()).Returns(queue.Object);

                var controller = mock.Create<RabbitLottoController>();
                var result = await controller.Get();

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.DrawNumber);
                Assert.AreEqual(drawTime, result.DrawDateTime);
                Assert.AreEqual(DrawStatusCode.Closed, result.DrawStatus);
                Assert.AreEqual(2, result.DrawWinningNumbers.ElementAt(0));
                Assert.AreEqual(3, result.DrawWinningNumbers.ElementAt(1));
                Assert.AreEqual(4, result.DrawWinningNumbers.ElementAt(2));
            }
        }

        [TestMethod]
        public async Task QueueCacheGet_TestQueue()
        {
            using (var mock = AutoMock.GetLoose())
            {
                DateTime drawTime = DateTime.Now;

                var cache = mock.Mock<ICacheStore>();
                cache.Setup(x => x.GetObject<RabbitLottoDrawModel>()).Returns(Task.FromResult((RabbitLottoDrawModel)null));
                mock.Mock<ICacheProvider>()
                    .Setup(x => x.GetStore(It.IsAny<int>())).Returns(cache.Object);

                // Should return the queued results now
                var queue = mock.Mock<ISimpleQueue<RabbitLottoDrawModel>>();
                queue.Setup(x => x.Receive()).Returns(Task.FromResult(new RabbitLottoDrawModel()
                {
                    DrawNumber = 2,
                    DrawDateTime = drawTime,
                    DrawStatus = DrawStatusCode.Open,
                    DrawWinningNumbers = new List<int>()
                }));
                mock.Mock<ISimpleQueueProvider>()
                    .Setup(x => x.GetQueue<RabbitLottoDrawModel>()).Returns(queue.Object);

                var controller = mock.Create<RabbitLottoController>();
                var result = await controller.Get();

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(2, result.DrawNumber);
                Assert.AreEqual(drawTime, result.DrawDateTime);
                Assert.AreEqual(DrawStatusCode.Open, result.DrawStatus);
                Assert.AreEqual(0, result.DrawWinningNumbers.Count());
            }
        }
    }
}
