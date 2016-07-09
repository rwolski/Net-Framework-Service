using Autofac;
using Framework.ServiceBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Framework.ServiceBus.ServiceBusModule;

namespace Framework.Tests
{
    [TestClass]
    public sealed class ServiceBusTests : FrameworkUnitTest
    {
        static int _test1Action = 0;
        static string _test2Action = null;

        #region Old Pub/Sub

        //public interface ITestEvent1 : IServiceEvent
        //{
        //    int Val { get; }
        //}

        //public interface ITestEvent2 : IServiceEvent
        //{
        //    string Val { get; }
        //}

        //public class TestEvent1 : ITestEvent1
        //{
        //    public ILifetimeScope Scope { get; set; }
        //    public int Val { get; set; }

        //    public Task Action()
        //    {
        //        _test1Action = Val;
        //        return Task.FromResult(0);
        //    }
        //}

        //public class TestEvent2 : ITestEvent2
        //{
        //    public ILifetimeScope Scope { get; set; }
        //    public string Val { get; set; }

        //    public Task Action()
        //    {
        //        _test2Action = Val;
        //        return Task.FromResult(0);
        //    }
        //}

        //[TestMethod]
        //[TestCategory("QueueTests")]
        //public async Task MassTransitPubSubTest()
        //{
        //    _test1Action = 0;
        //    _test2Action = null;

        //    const string queue = "MassTransitPublishTest";

        //    var entity1 = new TestEvent1()
        //    {
        //        Val = 2
        //    };
        //    var entity2 = new TestEvent2()
        //    {
        //        Val = "blah"
        //    };

        //    var provider = Container.Resolve<IServiceBusProvider>();
        //    var store = provider.GetBus(queue);

        //    await store.Publish(entity1);
        //    await store.Publish(entity2);

        //    int counter = 0;
        //    while ((_test1Action == 0 || _test2Action == null) && counter < 5)
        //    {
        //        System.Threading.Thread.Sleep(500);
        //        counter++;
        //    }

        //    Assert.AreEqual(2, _test1Action);
        //    Assert.AreEqual("blah", _test2Action);
        //}

        #endregion

        #region Pub/Sub

        public interface ITestContract1 : IMessageContract
        {
            int Val { get; }
        }

        public class TestContract1 : ITestContract1
        {
            public int Val { get; set; }
        }

        public interface ITestContract2 : IMessageContract
        {
            string Val { get; }
        }

        public class TestContract2 : ITestContract2
        {
            public string Val { get; set; }
        }

        public class ServiceContractAction1 : MessageAction<ITestContract1>
        {
            public ServiceContractAction1(ITestContract1 contract, ILifetimeScope scope)
                : base(contract)
            {
            }

            public override Task Action()
            {
                _test1Action = Contract.Val;
                return Task.FromResult(0);
            }
        }

        public class ServiceContractAction2 : MessageAction<ITestContract2>
        {
            public ServiceContractAction2(ITestContract2 contract, ILifetimeScope scope)
                : base(contract)
            {
            }

            public override Task Action()
            {
                _test2Action = Contract.Val;
                return Task.FromResult(0);
            }
        }


        [TestMethod]
        [TestCategory("ServiceBus")]
        public async Task MassTransit_PubSubTest()
        {
            _test1Action = 0;
            _test2Action = null;

            var bus = Container.Resolve<IServiceBusProvider>().GetBus("MassTransit_PubSubTest");

            var model1 = new TestContract1()
            {
                Val = 2
            };
            var model2 = new TestContract2()
            {
                Val = "blah"
            };

            // Publish both data contracts - one as object and another cast as interface
            await bus.Publish(model1);
            await bus.Publish<ITestContract2>((ITestContract2)model2);

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
        [TestCategory("ServiceBus")]
        public async Task MassTransit_AnonymousPubSubTest()
        {
            _test1Action = 0;

            var bus = Container.Resolve<IServiceBusProvider>().GetBus("MassTransit_AnonymousPubSubTest");

            var model1 = new
            {
                Val = 2
            };

            // Publish both data contracts - one as object and another cast as interface
            await bus.Publish<ITestContract1>(model1);

            int counter = 0;
            while (_test1Action == 0 && counter < 5)
            {
                System.Threading.Thread.Sleep(500);
                counter++;
            }

            Assert.AreEqual(2, _test1Action);
        }

        #endregion

        

        [TestMethod]
        [TestCategory("ServiceBus")]
        public async Task MassTransit_RequestResponseTest()
        {
            _test1Action = 0;
            _test2Action = null;

            try
            {

                var bus = Container.Resolve<IServiceBus>();

                var request = new DoubleMeRequest()
                {
                    Val = 2
                };
                var entity1 = await bus.Request<IDoubleMeRequest, IDoubleMeRequest>(request);

                Assert.AreEqual(4, entity1.Val);
            }
            catch (Exception e)
            {
                var i = 3;
            }
        }
    }
}

