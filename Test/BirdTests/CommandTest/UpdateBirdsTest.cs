using Application.Commands.Birds.UpdateBird;
using Application.Commands.Birds.UpdatedBird;
using Application.Dtos;
using Infrastructure.Database;

// Make sure to include the necessary using statements for your classes and dependencies

namespace Test.BirdTests.CommandTest
{
    [TestFixture]
    public class UpdateBirdTest
    {
        private UpdateBirdByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _handler = new UpdateBirdByIdCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task UpdateBird_ExistingId_ReturnsUpdatedBird()
        {
            // Arrange
            var existingBirdId = _mockDatabase.Birds.First().Id; // Assuming the first bird in the list is the one you want to update

            var updatedBirdDto = new BirdDto
            {
                Name = "UpdatedBirdName",
                CanFly = true
            };

            var command = new UpdateBirdByIdCommand(updatedBirdDto, existingBirdId);

            // Act
            var updatedBird = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(updatedBird); // Ensure an updated bird is returned
            Assert.That(updatedBird.Id, Is.EqualTo(existingBirdId)); // Ensure the ID of the updated bird matches the provided ID
            Assert.That(updatedBird.Name, Is.EqualTo(updatedBirdDto.Name));// Ensure the name was updated correctly
            Assert.That(updatedBird.CanFly, Is.EqualTo(updatedBirdDto.CanFly)); // Ensure the CanFly property was updated correctly
        }

        [Test]
        public async Task UpdateBird_NonExistingId_ReturnsNull()
        {
            // Arrange
            var nonExistingBirdId = Guid.NewGuid();

            var updatedBirdDto = new BirdDto
            {
                Name = "UpdatedBirdName",
                CanFly = true
            };

            var command = new UpdateBirdByIdCommand(updatedBirdDto, nonExistingBirdId);

            // Act
            var updatedBird = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Null(updatedBird); // Ensure no bird was updated and null is returned for a non-existing ID
        }
    }
}