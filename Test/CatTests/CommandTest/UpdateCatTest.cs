//using Application.Commands.Cats.UpdateCat;
//using Application.Dtos;
//using Infrastructure.Database;

//// Make sure to include the necessary using statements for your classes and dependencies

//namespace Test.CatTests.CommandTest
//{
//    [TestFixture]
//    public class UpdateCatTest
//    {
//        private UpdateCatByIdCommandHandler _handler;
//        private MockDatabase _mockDatabase;

//        [SetUp]
//        public void SetUp()
//        {
//            _mockDatabase = new MockDatabase();
//            _handler = new UpdateCatByIdCommandHandler(_mockDatabase);
//        }

//        [Test]
//        public async Task UpdateCat_ExistingId_ReturnsUpdatedCat()
//        {
//            // Arrange
//            var existingCatId = _mockDatabase.Cats.First().AnimalId; // Assuming the first cat in the list is the one you want to update

//            var updatedCatDto = new CatDto
//            {
//                Name = "UpdatedCatName",
//                LikesToPlay = true
//            };

//            var command = new UpdateCatByIdCommand(updatedCatDto, existingCatId);

//            // Act
//            var updatedCat = await _handler.Handle(command, CancellationToken.None);

//            // Assert
//            Assert.NotNull(updatedCat); // Ensure an updated cat is returned
//            Assert.That(updatedCat.AnimalId, Is.EqualTo(existingCatId)); // Ensure the ID of the updated cat matches the provided ID
//            Assert.That(updatedCat.Name, Is.EqualTo(updatedCatDto.Name));// Ensure the name was updated correctly
//            Assert.That(updatedCat.LikesToPlay, Is.EqualTo(updatedCatDto.LikesToPlay)); // Ensure the LikesToPlay property was updated correctly
//        }

//        [Test]
//        public async Task UpdateCat_NonExistingId_ReturnsNull()
//        {
//            // Arrange
//            var nonExistingCatId = Guid.NewGuid();

//            var updatedCatDto = new CatDto
//            {
//                Name = "UpdatedCatName",
//                LikesToPlay = true
//            };

//            var command = new UpdateCatByIdCommand(updatedCatDto, nonExistingCatId);

//            // Act
//            var updatedCat = await _handler.Handle(command, CancellationToken.None);

//            // Assert
//            Assert.Null(updatedCat); // Ensure no cat was updated and null is returned for a non-existing ID
//        }
//    }
//}