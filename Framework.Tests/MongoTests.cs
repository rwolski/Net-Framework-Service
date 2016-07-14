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

        [PersistedEntity]
        public class MongoTestEntity1 : Entity
        {
            [EntityField]
            public int[] TestField1 { get; set; }

            [EntityField]
            public IList<int> TestField2 { get; set; }
        }

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
            provider.DropDatabaseAsync(_database);
        }

        [TestMethod]
        [TestCategory("MongoTests")]
        public void MongoListTest()
        {
            MongoEntityMapper.Map<Entity>();
            MongoEntityMapper.Map<MongoTestEntity1>();

            var provider = Container.Resolve<IDatabaseProvider>();
            var store = provider.GetDatabase(_database);
            store.DropCollection(_collection);
            var collection = store.GetCollection<MongoTestEntity1>(_collection);

            var findResult = collection.FindAll();
            Assert.AreEqual(0, findResult.Count());

            var entity1 = new MongoTestEntity1()
            {
                TestField1 = new []{ 10, 11, 12 },
                TestField2 = new List<int>(){ 13, 14, 15 }
            };
            collection.Save(entity1);
            var identity = entity1.Id;
            entity1 = null;

            // Test the find by id
            entity1 = collection.FindByIdentity(identity);
            Assert.IsNotNull(entity1);
            Assert.AreEqual(10, entity1.TestField1[0]);
            Assert.AreEqual(11, entity1.TestField1[1]);
            Assert.AreEqual(12, entity1.TestField1[2]);
            Assert.AreEqual(13, entity1.TestField2[0]);
            Assert.AreEqual(14, entity1.TestField2[1]);
            Assert.AreEqual(15, entity1.TestField2[2]);

            store.DropCollection(_collection);
            provider.DropDatabaseAsync(_database);
        }
    }
}

