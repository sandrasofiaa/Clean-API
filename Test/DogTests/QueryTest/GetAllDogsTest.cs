using Application.Queries.Dogs;
using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Infrastructure.Database;

namespace Test.DogTests.QueryTest
{
    [TestFixture]
    public class GetAllDogsTest
    {
        private GetAllDogsQueryHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _handler = new GetAllDogsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ReturnsAllDogsFromMockDatabase()
        {
            // Arrange
            var dogsList = _mockDatabase.Dogs;

            var query = new GetAllDogsQuery();

            // Act
            List<Dog> result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(dogsList.Count));
        }
    }
}