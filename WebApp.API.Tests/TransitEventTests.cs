using Autofac;
using Autofac.Extras.Moq;
using Framework.ServiceBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApp.API.Models;
using WebApp.API.ServiceHandler;
using static Framework.ServiceBus.ServiceBusModule;

namespace WebApp.API.Tests
{
    [TestClass]
    public class PowerballControllerTest : ControllerUnitTest
    {
        [TestMethod]
        public async Task TransitEventTest()
        {
            var bus = Container.Resolve<IServiceBusProvider>().GetBus("TestServiceBus");

            //var eventHandler = Container.Resolve<TransitLottoDrawEventServiceHandler>();

            var model = new TransitLottoDrawEventServiceHandler()
            {
                DrawDateTime = DateTime.Now,
                DrawNumber = 16,
                DrawStatus = DrawStatusCode.Open,
                DrawWinningNumbers = new[] { 1, 2, 3 },
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
                Version = 1
            };

            await bus.Publish(model);

            Thread.Sleep(5000);
        }



        [TestMethod]
        public async Task TransitContractTest()
        {
            var bus = Container.Resolve<IServiceBusProvider>().GetBus("TestServiceBus2");

            var model = new TestContract()
            {
                Val = 2
            };

            await bus.Publish<IServiceContract>((IServiceContract)model);

            Thread.Sleep(5000);
        }
    }
}
