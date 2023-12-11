using Application.Commands.Dogs;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;

namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class AddDogTests
    {
        private AddDogCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new AddDogCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task GivenValidDogDto_AddDogReturnsNewDog()
        {
            // Arrange
            var DogDto = new DogDto { Name = "Fido" };
            var addDogCommand = new AddDogCommand(DogDto);

            // Act
            Dog addedDog = await _handler.Handle(addDogCommand, CancellationToken.None);

            // Assert
            Assert.IsNotNull(addedDog);
            Assert.That(addedDog.Name, Is.EqualTo(DogDto.Name));
            Assert.IsTrue(_mockDatabase.Dogs.Contains(addedDog));
        }

        [Test]
        public void Handle_EmptyNameWhenAddingNewDog_ThrowsArgumentException()
        {
            // Arrange
            var mockDatabase = new MockDatabase();
            var handler = new AddDogCommandHandler(mockDatabase);
            var command = new AddDogCommand(new DogDto { Name = "" });

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(command, CancellationToken.None));
        }
    }

}
