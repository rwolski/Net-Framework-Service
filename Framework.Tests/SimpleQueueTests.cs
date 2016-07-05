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
    public sealed class SimpleQueueTests : FrameworkUnitTest
    {
        [QueuedEntity("TestEntity")]
        public class QueueTestEntity
        {
            [EntityField]
            public int TestField { get; set; }
        }

        [TestMethod]
        [TestCategory("QueueTests")]
        public async Task RabbitMQLifecycleTest()
        {
            const string _queue = "TestEntity1";

            var entity = new QueueTestEntity()
            {
                TestField = 2
            };

            var provider = Container.Resolve<ISimpleQueueProvider>();
            var store = provider.GetQueue<QueueTestEntity>(_queue);

            await store.Send(entity);

            System.Threading.Thread.Sleep(100);

            var result = await store.Receive();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.TestField);
        }
    }
}

