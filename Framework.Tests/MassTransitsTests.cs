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
    public sealed class MassTransitTests : FrameworkUnitTest
    {
        const string _queue = "TestEntity";
        const string _collection = "TestCollection";

        [QueuedEntity("TestEntity")]
        public class QueueTestEntity
        {
            [EntityField]
            public int TestField { get; set; }
        }

        [TestMethod]
        [TestCategory("QueueTests")]
        public void MassTransitLifecycleTest()
        {
            var entity = new QueueTestEntity()
            {
                TestField = 2
            };

            var provider = Container.Resolve<IQueueProvider>();
            var store = provider.GetQueue<QueueTestEntity>(_queue);
            
            store.Send(entity);

            System.Threading.Thread.Sleep(100);

            var result = store.Receive();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.TestField);
        }
    }
}

