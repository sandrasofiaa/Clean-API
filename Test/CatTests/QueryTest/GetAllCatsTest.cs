using Application.Queries.Cats;
using Application.Queries.Cats.GetAll;
using Domain.Models;
using Infrastructure.Database;
using MediatR;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Test.CatTests.QueryTest
{
    [TestFixture]
    public class GetAllCatsTest
    {
        private GetAllCatsQueryHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _handler = new GetAllCatsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ReturnsAllCatsFromMockDatabase()
        {
            // Arrange
            var catsList = _mockDatabase.Cats;

            var query = new GetAllCatsQuery();

            // Act
            List<Cat> result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(catsList.Count));
            Assert.IsTrue(result.SequenceEqual(catsList)); // Check if both lists have the same elements
        }
    }
}

