using Autofac;
using Framework.ServiceBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;
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

            var model = new TestContract1()
            {
                Val = 2
            };

            //await bus.Publish<TestContract1>((TestContract1)model);
            await bus.Publish(model);

            var model1 = new TestContract2()
            {
                Val = "blah"
            };

            await bus.Publish<TestContract2> ((TestContract2)model1);
            //await bus.Publish(model1);

            Thread.Sleep(5000);
        }
    }
}
