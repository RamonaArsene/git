using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebStore.Common.Entities;
using WebStore.Common.Services;

namespace WebStore.Tests
{
    public abstract class EntityServiceTests<TEntity, TEntityService>
        where TEntity : IEntity
        where TEntityService: IEntityService<TEntity>
    {
        protected abstract TEntityService CreateTarget();
        protected abstract TEntity CreateInput();
        protected abstract void AssertAreEqual(TEntity expected, TEntity actual);

        public virtual void InitializeTest()
        {

        }

        public virtual void CleanupTest()
        {

        }

        [TestMethod]
        public void WhenInserting1WeGetTheSameFromDb()
        {
            var target = CreateTarget();

            var expected = CreateInput();

            target.Save(expected);

            var actual = target.GetById(expected.Id);

            Assert.IsNotNull(actual);
            AssertAreEqual(expected, actual);
        }
    }
}
