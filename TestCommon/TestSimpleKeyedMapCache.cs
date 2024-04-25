using System;
using ISSProject.Common;
using ISSProject.Common.Cache;
using ISSProject.GraphAnalyser.Domain;

namespace TestCommon
{
    internal class Person : IKeyedEntity<int>
    {
        public int Id { get; }
        public string Name { get; }
        public DateTime BirthDate { get; }

        public Person(int id, string name, DateTime birthDate)
        {
            Id = id;
            Name = name;
            BirthDate = birthDate;
        }

        public int GetId()
        {
            return Id;
        }

        public object Clone()
        {
            // Implement cloning logic if needed
            return new Person(Id, Name, BirthDate);
        }
    }

    [TestClass]
    public class TestSimpleKeyedMapCache
    {
        private SimpleKeyedMapCache<Person, int> cache;

        [TestInitialize]
        public void TestInitialize()
        {
            cache = new SimpleKeyedMapCache<Person, int>();
        }
        [TestMethod]
        public void Add_WhenItemNotInCache_ShouldReturnTrueAndAddItem()
        {
            var person = new Person(1, "John Doe", new DateTime(1990, 1, 1));
            var result = cache.Add(person);
            Assert.IsTrue(result);
            Assert.IsTrue(cache.Any(person.GetId()));
        }

        [TestMethod]
        public void Add_WhenItemAlreadyInCache_ShouldReturnFalseAndNotAddItem()
        {
            var person = new Person(1, "John Doe", new DateTime(1990, 1, 1));
            cache.Add(person);

            var result = cache.Add(person);

            Assert.IsFalse(result);
            Assert.AreEqual(1, cache.GetCache().Count);
        }

        [TestMethod]
        public void Update_WhenItemInCache_ShouldReturnTrueAndUpdateItem()
        {
            var person = new Person(1, "John Doe", new DateTime(1990, 1, 1));
            cache.Add(person);
            var updatedPerson = new Person(1, "John Doe Updated", new DateTime(1990, 1, 1));
            var result = cache.Update(updatedPerson);
            Assert.IsTrue(result);
            Assert.AreEqual(updatedPerson.GetId(), cache.ById(updatedPerson.GetId()).GetId());
            Assert.AreEqual(updatedPerson.Name, cache.ById(updatedPerson.GetId()).Name);
            Assert.AreEqual(updatedPerson.BirthDate, cache.ById(updatedPerson.GetId()).BirthDate);
        }

        [TestMethod]
        public void Remove_WhenItemInCache_ShouldReturnTrueAndRemoveItem()
        {
            var person = new Person(1, "John Doe", new DateTime(1990, 1, 1));
            cache.Add(person);

            var result = cache.Remove(person);

            Assert.IsTrue(result);
            Assert.IsFalse(cache.Any(person.GetId()));
        }

        [TestMethod]
        public void Remove_WhenItemNotInCache_ShouldReturnFalse()
        {
            var person = new Person(1, "John Doe", new DateTime(1990, 1, 1));

            var result = cache.Remove(person);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ById_WhenItemInCache_ShouldReturnCorrectItem()
        {
            var person = new Person(1, "John Doe", new DateTime(1990, 1, 1));
            cache.Add(person);
            var retrievedPerson = cache.ById(person.GetId());
            Assert.AreEqual(person.Name, retrievedPerson.Name);
            Assert.AreEqual(person.BirthDate, retrievedPerson.BirthDate);
            Assert.AreEqual(person.Id, retrievedPerson.Id);
        }

        [TestMethod]
        public void ById_WhenItemNotInCache_ShouldThrowKeyNotFoundException()
        {
            var nonExistingId = 999;

            Assert.ThrowsException<KeyNotFoundException>(() => { cache.ById(nonExistingId); });
        }

        [TestMethod]
        public void Any_WhenItemInCache_ShouldReturnTrue()
        {
            var cache = new SimpleKeyedMapCache<Person, int>();
            var person = new Person(1, "John Doe", new DateTime(1990, 1, 1));
            cache.Add(person); // Add person to cache

            var result = cache.Any(person.GetId());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Any_WhenItemNotInCache_ShouldReturnFalse()
        {
            var cache = new SimpleKeyedMapCache<Person, int>();
            var person = new Person(1, "John Doe", new DateTime(1990, 1, 1));

            var result = cache.Any(person.GetId());

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetCache_ShouldReturnCorrectCacheContent()
        {
            var cache = new SimpleKeyedMapCache<Person, int>();
            var person1 = new Person(1, "John Doe", new DateTime(1990, 1, 1));
            var person2 = new Person(2, "Jane Doe", new DateTime(1995, 5, 10));
            cache.Add(person1);
            cache.Add(person2);

            var cacheContent = cache.GetCache();

            Assert.AreEqual(2, cacheContent.Count);
            Assert.IsTrue(cacheContent.ContainsKey(person1.GetId()));
            Assert.IsTrue(cacheContent.ContainsKey(person2.GetId()));
        }
    }
}