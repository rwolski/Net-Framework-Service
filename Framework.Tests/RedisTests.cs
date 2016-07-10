using Microsoft.VisualStudio.TestTools.UnitTesting;
using Framework.Tests;
using Framework.Cache;
using Autofac;
using System.Threading.Tasks;

namespace Framework.Tests
{
    [TestClass]
    public sealed class RedisTests : FrameworkUnitTest
    {
        readonly int _databaseIndex = AppSettings.RedisDatabaseIndex;

        [TestMethod]
        [TestCategory("CacheTests")]
        public async Task RedisReadWriteTest()
        {
            var provider = Container.Resolve<ICacheProvider>();
            var store = provider.GetStore(_databaseIndex);

            await store.SetString("TestKey1", "TestVal1");

            var val = await store.GetString("TestKey1");
            Assert.AreEqual("TestVal1", val);

            await store.Unset("TestKey1");

            val = await store.GetString("TestKey1");
            Assert.IsNull(val);
        }
    }
}
