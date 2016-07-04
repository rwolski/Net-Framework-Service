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
    public sealed class QueueTests : FrameworkUnitTest
    {
        [QueuedEntity("TestEntity")]
        public class QueueTestEntity
        {
            [EntityField]
            public int TestField { get; set; }
        }

        [TestMethod]
        [TestCategory("QueueTests")]
        public void RabbitMQLifecycleTest()
        {
            const string _queue = "TestEntity1";

            var entity = new QueueTestEntity()
            {
                TestField = 2
            };

            var provider = Container.ResolveKeyed<IQueueProvider>(QueueProviderType.RabbitMQ);
            var store = provider.GetQueue<QueueTestEntity>(_queue);

            store.Send(entity);

            System.Threading.Thread.Sleep(100);

            var result = store.Receive().Result;
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.TestField);
        }

        [TestMethod]
        [TestCategory("QueueTests")]
        public void MassTransitLifecycleTest()
        {
            const string _queue = "TestEntity2";

            var entity = new QueueTestEntity()
            {
                TestField = 2
            };

            var provider = Container.ResolveKeyed<IQueueProvider>(QueueProviderType.MassTransit);
            var store = provider.GetQueue<QueueTestEntity>(_queue);

            //store.Publish(entity);
            store.Send(entity);

            System.Threading.Thread.Sleep(100);

            var result = store.Request().Result;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Body);
            Assert.AreEqual(2, result.Body.TestField);
        }
    }
}

