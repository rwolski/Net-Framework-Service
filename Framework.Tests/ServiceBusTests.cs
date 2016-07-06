using Autofac;
using Framework.ServiceBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Framework.Tests
{
    [TestClass]
    public sealed class ServiceBusTests : FrameworkUnitTest
    {
        static int _test1Action = 0;
        static string _test2Action = null;

        public class TestData1 : ServiceData
        {
            public int Val { get; set; }

            public override Task Action()
            {
                _test1Action = Val;
                return Task.FromResult(0);
            }
        }

        public class TestData2 : ServiceData
        {
            public string Val { get; set; }

            public override Task Action()
            {
                _test2Action = Val;
                return Task.FromResult(0);
            }
        }

        [TestMethod]
        [TestCategory("QueueTests")]
        public async Task MassTransitLifecycleTest()
        {
            _test1Action = 0;
            _test2Action = null;

            const string queue = "MassTransitPublishTest";

            var entity1 = new TestData1()
            {
                Val = 2
            };
            var entity2 = new TestData2()
            {
                Val = "blah"
            };

            var provider = Container.Resolve<IServiceBusProvider>();
            var store = provider.GetBus(queue);

            await store.Publish(entity1);
            await store.Publish(entity2);

            int counter = 0;
            while ((_test1Action == 0 || _test2Action == null) && counter < 5)
            {
                System.Threading.Thread.Sleep(500);
                counter++;
            }

            Assert.AreEqual(2, _test1Action);
            Assert.AreEqual("blah", _test2Action);
        }
    }
}

