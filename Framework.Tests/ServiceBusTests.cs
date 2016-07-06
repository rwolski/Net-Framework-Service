using Autofac;
using Framework.ServiceBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System;

namespace Framework.Tests
{
    [TestClass]
    public sealed class ServiceBusTests : FrameworkUnitTest
    {
        static int _test1Action = 0;
        static string _test2Action = null;

        public interface ITestEvent1 : IServiceEvent
        {
            int Val { get; }
        }

        public interface ITestEvent2 : IServiceEvent
        {
            string Val { get; }
        }

        public class TestEvent1 : ITestEvent1
        {
            public ILifetimeScope Scope { get; set; }
            public int Val { get; set; }

            public Task Action()
            {
                _test1Action = Val;
                return Task.FromResult(0);
            }
        }

        public class TestEvent2 : ITestEvent2
        {
            public ILifetimeScope Scope { get; set; }
            public string Val { get; set; }

            public Task Action()
            {
                _test2Action = Val;
                return Task.FromResult(0);
            }
        }

        [TestMethod]
        [TestCategory("QueueTests")]
        public async Task MassTransitPubSubTest()
        {
            _test1Action = 0;
            _test2Action = null;

            const string queue = "MassTransitPublishTest";

            var entity1 = new TestEvent1()
            {
                Val = 2
            };
            var entity2 = new TestEvent2()
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

        [TestMethod]
        [TestCategory("QueueTests")]
        public async Task MassTransitAnonymousPubSubTest()
        {
            //_test1Action = 0;
            //_test2Action = null;

            //const string queue = "MassTransitPublishTest";

            //var entity1 = new 
            //{
            //    Val = 2
            //};
            //var entity2 = new 
            //{
            //    Val = "blah"
            //};

            //var provider = Container.Resolve<IServiceBusProvider>();
            //var store = provider.GetBus(queue);

            //await store.Publish<TestEvent1>(entity1);
            //await store.Publish<TestEvent2>(entity2);

            //int counter = 0;
            //while ((_test1Action == 0 || _test2Action == null) && counter < 5)
            //{
            //    System.Threading.Thread.Sleep(500);
            //    counter++;
            //}

            //Assert.AreEqual(2, _test1Action);
            //Assert.AreEqual("blah", _test2Action);
        }
    }
}

