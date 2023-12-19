//using Application.Commands.Cats.DeleteCat;
//using Infrastructure.Database;

//namespace Test.CatTests.CommandTest
//{
//    [TestFixture]
//    public class DeleteCatTest
//    {
//        private DeleteCatByIdCommandHandler _handler;
//        private MockDatabase _mockDatabase;

//        [SetUp]
//        public void SetUp()
//        {
//            _mockDatabase = new MockDatabase();
//            _handler = new DeleteCatByIdCommandHandler(_mockDatabase);
//        }

//        [Test]
//        public async Task DeleteSpecificCatById_ReturnsTrue()
//        {
//            // Arrange
//            var specificCatId = new Guid("87654321-4321-8765-4321-876543210123");
//            var command = new DeleteCatByIdCommand(specificCatId);

//            // Act
//            var result = await _handler.Handle(command, CancellationToken.None);

//            // Assert
//            Assert.IsTrue(result); // Ensure that the Cat was deleted and returns true
//            Assert.IsFalse(_mockDatabase.Cats.Any(b => b.AnimalId == specificCatId)); // Ensure the cat was removed from the mock database
//        }


//        [Test]
//        public async Task DeleteCatById_NonExistingId_ReturnsFalse()
//        {
//            // Arrange
//            var nonExistingCatId = new Guid("11111111-1111-1111-1111-111111111111"); // Use a Guid that doesn't exist in your mock data
//            var command = new DeleteCatByIdCommand(nonExistingCatId);

//            // Act
//            var result = await _handler.Handle(command, CancellationToken.None);

//            // Assert
//            Assert.IsFalse(result); // Ensure that no cat was deleted and returns false
//        }

//    }
//}