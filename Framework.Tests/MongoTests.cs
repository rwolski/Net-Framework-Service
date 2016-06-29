using Autofac;
using Framework.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Tests
{
    [TestClass]
    public sealed class MongoTests : FrameworkUnitTest
    {
        const string _database = "TestDatabase";
        const string _collection = "TestCollection";

        [PersistedEntity]
        public class MongoTestEntity : Entity
        {
            [EntityField]
            public int TestField { get; set; }
        }

        //[ClassInitialize]
        //public static void ClassInitialise(TestContext context)
        //{
        //    var provider = Container.Resolve<IDatabaseProvider>();
        //    var store = provider.GetDatabase(_database);
        //}

        //[ClassCleanup]
        //public static void ClassCleanup()
        //{
        //    var provider = Container.Resolve<IDatabaseProvider>();
        //    provider.DropDatabase(_database);
        //}

        [TestMethod]
        [TestCategory("MongoTests")]
        public void MongoLifecycleTest()
        {
            MongoEntityMapper.Map<Entity>();
            MongoEntityMapper.Map<MongoTestEntity>();

            var provider = Container.Resolve<IDatabaseProvider>();
            var store = provider.GetDatabase(_database);
            store.DropCollection(_collection);
            var collection = store.GetCollection<MongoTestEntity>(_collection);

            var findResult = collection.FindAll();
            Assert.AreEqual(0, findResult.Count());

            var entity1 = new MongoTestEntity()
            {
                TestField = 10
            };
            collection.Save(entity1);
            var identity = entity1.Id;
            entity1 = null;

            // Test the find by id
            entity1 = collection.FindByIdentity(identity);
            Assert.IsNotNull(entity1);

            var entity2 = new MongoTestEntity()
            {
                TestField = 20
            };
            collection.Save(entity2);
            var entity3 = new MongoTestEntity()
            {
                TestField = 30
            };
            collection.Save(entity3);

            // Test the update
            entity1.TestField = 15;
            collection.Save(entity1);

            // Test the find all
            findResult = collection.FindAll();
            Assert.AreEqual(3, findResult.Count());
            Assert.AreEqual(15, findResult.ElementAt(0).TestField);
            Assert.AreEqual(20, findResult.ElementAt(1).TestField);
            Assert.AreEqual(30, findResult.ElementAt(2).TestField);

            // Test the filter works
            findResult = collection.Find(new WhereCondition<MongoTestEntity>(x => x.TestField <= 20));

            Assert.AreEqual(2, findResult.Count());
            Assert.AreEqual(15, findResult.ElementAt(0).TestField);
            Assert.AreEqual(20, findResult.ElementAt(1).TestField);

            // Test the order by works
            findResult = collection.Find(null, new List<OrderBy<MongoTestEntity>>() {
                new OrderBy<MongoTestEntity>()
                {
                    Exp = x => x.TestField,
                    Ascending = false
                }
            });

            Assert.AreEqual(3, findResult.Count());
            Assert.AreEqual(30, findResult.ElementAt(0).TestField);
            Assert.AreEqual(20, findResult.ElementAt(1).TestField);
            Assert.AreEqual(15, findResult.ElementAt(2).TestField);

            store.DropCollection(_collection);
            provider.DropDatabase(_database);
        }
    }
}

