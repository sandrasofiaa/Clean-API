using Application.Commands.Birds.AddBird;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;

namespace Test.BirdTests.CommandTest
{
    [TestFixture]
    public class AddBirdTests
    {
        private AddBirdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new AddBirdCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task GivenValidBirdDto_AddBird_ReturnsNewBird()
        {
            // Arrange
            var birdDto = new BirdDto { Name = "Parrot", CanFly = true };
            var addBirdCommand = new AddBirdCommand(birdDto);

            // Act
            Bird addedBird = await _handler.Handle(addBirdCommand, CancellationToken.None);

            // Assert
            Assert.IsNotNull(addedBird);
            Assert.That(addedBird.Name, Is.EqualTo(birdDto.Name));
            Assert.That(addedBird.CanFly, Is.EqualTo(birdDto.CanFly));
            Assert.IsTrue(_mockDatabase.Birds.Contains(addedBird));
        }

        [Test]
        public void GivenInvalidBirdDto_AddBird_ThrowsArgumentException()
        {
            // Arrange
            var invalidBirdDto = new BirdDto { Name = "", CanFly = false };
            var addBirdCommand = new AddBirdCommand(invalidBirdDto);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _handler.Handle(addBirdCommand, CancellationToken.None));
        }
    }
}