using Microsoft.VisualStudio.TestTools.UnitTesting;
using Framework.Tests;
using Framework.Cache;
using Autofac;

namespace Framework.Tests
{
    [TestClass]
    public sealed class RedisTests : FrameworkUnitTest
    {
        [TestMethod]
        public void RedisReadWriteTest()
        {
            var provider = Container.Resolve<RedisProvider>();
            var store = provider.GetStore(10);

            store.SetString("TestKey1", "TestVal1");

            var val = store.GetString("TestKey1");
            Assert.AreEqual("TestVal1", val);
        }
    }
}
