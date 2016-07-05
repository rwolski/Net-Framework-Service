using Autofac;
using Framework.Data;
using Framework.Queue;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Tests
{
    [TestClass]
    public sealed class ServiceBusTests : FrameworkUnitTest
    {
        public class TestAction : QueueAction
        {
            public int TestField { get; set; }

            public override void PerformAction()
            {
                int i = 3;
            }
        }

        [TestMethod]
        [TestCategory("QueueTests")]
        public async Task MassTransitLifecycleTest()
        {
            const string _queue = "TestEntity2";

            var entity = new QueueTestMessage<int>()
            {
                TestField = 2
            };

            var provider = Container.ResolveKeyed<IServiceBusProvider>(QueueProviderType.MassTransit);
            var store = provider.GetQueue<QueueMessage<TestAction>>(_queue);

            //var provider1 = Container.ResolveKeyed<IQueueProvider>(QueueProviderType.RabbitMQ);
            //var store1 = provider.GetQueue<QueueTestMessage>(_queue);

            //await store.Send(entity);

            await store.Publish(entity);

            System.Threading.Thread.Sleep(100);

            //var result = store.Request();
            //var result = store.Request().Result;
            //Assert.IsNotNull(result);
            //Assert.IsNotNull(result.Body);
            //Assert.AreEqual(2, result.Body.TestField);
        }
    }
}

