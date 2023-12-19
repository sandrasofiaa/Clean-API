//using Application.Queries.Birds;
//using Application.Queries.Birds.GetAll;
//using Domain.Models;
//using Infrastructure.Database;

//namespace Test.BirdTests.QueryTest
//{
//    [TestFixture]
//    public class GetAllBirdsTest
//    {
//        private GetAllBirdsQueryHandler _handler;
//        private MockDatabase _mockDatabase;

//        [SetUp]
//        public void SetUp()
//        {
//            _mockDatabase = new MockDatabase();
//            _handler = new GetAllBirdsQueryHandler(_mockDatabase);
//        }

//        [Test]
//        public async Task Handle_ReturnsAllBirdsFromMockDatabase()
//        {
//            // Arrange
//            var birdsList = _mockDatabase.Birds;

//            var query = new GetAllBirdsQuery();

//            // Act
//            List<Bird> result = await _handler.Handle(query, CancellationToken.None);

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.That(result.Count, Is.EqualTo(birdsList.Count));
//            Assert.IsTrue(result.SequenceEqual(birdsList)); // Check if both lists have the same elements
//        }
//    }
//}

