﻿//using Application.Queries.Birds.GetById;
//using Infrastructure.Database;

//namespace Test.BirdTests.QueryTest
//{
//    [TestFixture]
//    public class GetBirdByIdTests
//    {
//        private GetByBirdIdQueryHandler _handler;
//        private MockDatabase _mockDatabase;

//        [SetUp]
//        public void SetUp()
//        {
//            // Initialize the handler and mock database before each test
//            _mockDatabase = new MockDatabase();
//            _handler = new GetByBirdIdQueryHandler(_mockDatabase);
//        }

//        //Returns Null
//        [Test]
//        public async Task Handle_ValidId_ReturnsCorrectBird()
//        {
//            // Arrange
//            var birdId = new Guid("12345678-1234-5678-1234-567812345676");

//            var query = new GetBirdByIdQuery(birdId);

//            // Act
//            var result = await _handler.Handle(query, CancellationToken.None);

//            // Assert
//            Assert.That(result, Is.Not.Null);
//            Assert.That(result.AnimalId, Is.EqualTo(birdId));
//        }

//        [Test]
//        public async Task Handle_InvalidId_ReturnsNull()
//        {
//            // Arrange
//            var invalidBirdId = Guid.NewGuid();

//            var query = new GetBirdByIdQuery(invalidBirdId);

//            // Act
//            var result = await _handler.Handle(query, CancellationToken.None);

//            // Assert
//            Assert.IsNull(result);
//        }
//    }
//}
