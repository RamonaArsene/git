using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Database.V2.Tests
{
    [TestClass]
    public class UnitTest1
    {
        protected IDatabase CreateTestTarget()
        {
            return new DatabaseImpl();
        }

        [TestMethod]
        public void WhenInsertingWeCanRetrieveSameProperties()
        {
            var database = CreateTestTarget();

            var expected = new Dosar { Id = 1, Code = "PI13232", Description = "Dosar intern 1" };

            database.Insert(expected);

            var actual = database.Get<Dosar>(expected.Id);

            Assert.IsNotNull(actual);
            actual.AssertAllPropertiesAreEqual(expected);
        }

        [TestMethod]
        public void WhenUpdatingWeRetrieveSameThing()
        {
            var database = CreateTestTarget();

            var initialInput = new Dosar { Id = 1, Code = "PI13232", Description = "Dosar intern 1" };

            database.Insert(initialInput);

            var expected = new Dosar { Id = 1, Code = "PI13232555", Description = "Dosar intern 1555" };

            database.Update(expected);

            var actual = database.Get<Dosar>(expected.Id);

            Assert.IsNotNull(actual);
            actual.AssertAllPropertiesAreEqual(expected);
        }


        [TestMethod]
        public void WhenDeletingWeRetrieveNull()
        {
            var database = CreateTestTarget();

            var input = new Dosar { Id = 1, Code = "PI13232", Description = "Dosar intern 1" };

            database.Insert(input);
            database.Delete(input.Id);

            var actual = database.Get<Dosar>(input.Id);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void WhenUpdatingNonExistingNoExceptionIsThrown()
        {
            var database = CreateTestTarget();

            var expected = new Dosar { Id = 1, Code = "PI13232555", Description = "Dosar intern 1555" };

            database.Update(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateEntityInDatabaseException))]
        public void WhenInsertingTwoIdenticalKeysWegetException()
        {
            var database = CreateTestTarget();

            var input1 = new Dosar { Id = 1, Code = "PI13232", Description = "Dosar intern 1" };
            var input2 = new Dosar { Id = 1, Code = "XXXX", Description = "XXXX1" };

            database.Insert(input1);
            database.Insert(input2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenInsertingNullWeGetException()
        {
            var database = CreateTestTarget();

            database.Insert<Dosar>(null);
        }

        [TestMethod]
        public void WhenInsertingTwoDosareWeGetAllCount2()
        {
            var database = CreateTestTarget();

            var input1 = new Dosar { Id = 1, Code = "PI13232", Description = "Dosar intern 1" };
            var input2 = new Dosar { Id = 2, Code = "XXXX", Description = "XXXX1" };

            database.Insert(input1);
            database.Insert(input2);

            var actualAll = database.GetAll<Dosar>();

            Assert.AreEqual(2, actualAll.Count);
        }

        [TestMethod]
        public void WhenInsertingTwoDosareAndDeleting1WeGetAllCount1()
        {
            var database = CreateTestTarget();

            var input1 = new Dosar { Id = 1, Code = "PI13232", Description = "Dosar intern 1" };
            var input2 = new Dosar { Id = 2, Code = "XXXX", Description = "XXXX1" };

            database.Insert(input1);
            database.Insert(input2);

            database.GetAll<Dosar>();

            database.Delete(input1.Id);

            var actualAll = database.GetAll<Dosar>();

            Assert.AreEqual(1, actualAll.Count);
        }

    }
}
