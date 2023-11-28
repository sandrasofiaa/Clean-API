using Application.Queries.Birds.GetById;
using Application.Queries.Cats.GetById;
using Domain.Models;
using Infrastructure.Database;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Test.CatTests.QueryTest
{
    [TestFixture]
    public class GetCatByIdTests
    {
        private GetCatBydIdQueryHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new GetCatBydIdQueryHandler(_mockDatabase);
        }

        //Returns Null
        [Test]
        public async Task Handle_ValidId_ReturnsCorrectCat()
        {
            // Arrange
            var catId = new Guid("87654321-4321-8765-4321-876543210124");

            var query = new GetCatByIdQuery(catId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(catId));
        }

        [Test]
        public async Task Handle_InvalidId_ReturnsNull()
        {
            // Arrange
            var invalidCatId = Guid.NewGuid();

            var query = new GetCatByIdQuery(invalidCatId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNull(result);
        }
    }
}
