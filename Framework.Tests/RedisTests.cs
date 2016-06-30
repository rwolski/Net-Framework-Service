using Microsoft.VisualStudio.TestTools.UnitTesting;
using Framework.Tests;
using Framework.Cache;
using Autofac;

namespace Framework.Tests
{
    [TestClass]
    public sealed class RedisTests : FrameworkUnitTest
    {
        readonly int _databaseIndex = AppSettings.RedisDatabaseIndex;

        [TestMethod]
        [TestCategory("CacheTests")]
        public void RedisReadWriteTest()
        {
            var provider = Container.Resolve<ICacheProvider>();
            var store = provider.GetStore(_databaseIndex);

            store.SetString("TestKey1", "TestVal1");

            var val = store.GetString("TestKey1");
            Assert.AreEqual("TestVal1", val);

            store.Unset("TestKey1");

            val = store.GetString("TestKey1");
            Assert.IsNull(val);
        }
    }
}
