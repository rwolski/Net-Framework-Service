using Autofac;
using Framework.ServiceBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Framework.Tests
{
    [TestClass]
    public sealed class ServiceBusTests : FrameworkUnitTest
    {
        public class TestData : IServiceData
        {
            public int Val { get; set; }

            public TestData()
            {
                var i = 3;
            }
            //public int Val { get; set; }

            public void Action()
            {
                var i = 3;
            }
        }

        //public class TestData2 : IServiceData
        //{
        //    public int Val { get; set; }

        //    public void Action()
        //    {
        //        var i = 3;
        //    }
        //}

        //public class TestMessage : QueueMessage<TestData>
        //{
        //    public TestMessage(ILifetimeScope scope)
        //        : base(scope)
        //    {
        //    }
        //}

        //public class TestAction : IServiceAction<IServiceData>
        //{
        //    public IServiceData Data { get; set; }

        //    public void PerformAction()
        //    {
        //        int i = 3;
        //    }
        //}

        [TestMethod]
        [TestCategory("QueueTests")]
        public async Task MassTransitLifecycleTest()
        {
            const string _queue = "TestEntity2";

            //using (var scope = Container.BeginLifetimeScope(builder =>
            //    {
            //        builder.RegisterType<TestData>().As<IServiceData>();
            //        builder.RegisterType<TestAction>().As<IServiceAction<IServiceData>>();
            //    }))
            //{
                var entity = new TestData()
                {
                    Val = 2
                };
                //var entity2 = new TestData2()
                //{
                //    Val = 2
                //};

            var provider = Container.Resolve<IServiceBusProvider>();
            var store = provider.GetBus(_queue);

            //var provider1 = Container.ResolveKeyed<IQueueProvider>(QueueProviderType.RabbitMQ);
            //var store1 = provider.GetQueue<QueueTestMessage>(_queue);

            //await store.Send(entity);

            await store.Publish(entity);
            //await store.Publish(entity2);

            System.Threading.Thread.Sleep(1000);

            //var result = store.Request();
            //var result = store.Request().Result;
            //Assert.IsNotNull(result);
            //Assert.IsNotNull(result.Body);
            //Assert.AreEqual(2, result.Body.TestField);
            //}
        }
    }
}

