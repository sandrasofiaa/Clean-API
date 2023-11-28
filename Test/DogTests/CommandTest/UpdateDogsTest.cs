using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;

namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class UpdateDogsTest
    {
        private UpdateDogByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new UpdateDogByIdCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task UpdateDogName_WhenValidIdProvided_UpdatesNameCorrectly()
        {
            // Arrange
            var initialDog = _mockDatabase.Dogs.FirstOrDefault(); // Accessing the first dog for demonstration
            var updatedDogDto = new DogDto { Name = "UpdatedName" };
            var updateCommand = new UpdateDogByIdCommand(updatedDogDto, initialDog?.Id ?? Guid.Empty);

            // Act
            var updatedDog = await _handler.Handle(updateCommand, CancellationToken.None);

            // Assert
            Assert.NotNull(updatedDog, "Updated dog should not be null");
            Assert.That(updatedDogDto.Name, Is.EqualTo(updatedDog.Name), "Dog name should be updated correctly");
        }



        //[Test]
        //public async Task UpdateNonExistingDogReturnsNull()
        //{
        //    // Arrange
        //    var nonExistingDogId = Guid.NewGuid();
        //    var updatedDogDto = new DogDto { Name = "UpdatedName" };
        //    var updateCommand = new UpdateDogByIdCommand(updatedDogDto, nonExistingDogId);

        //    // Act
        //    var updatedDog = await _handler.Handle(updateCommand, CancellationToken.None);

        //    // Assert
        //    Assert.Null(updatedDog);
        //}
    }
}