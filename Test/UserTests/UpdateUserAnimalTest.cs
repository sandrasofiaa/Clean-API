using Application.Commands.Users;
using Application.Handlers.Users;
using Infrastructure.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test.UserTests
{
    [TestFixture]
    public class UpdateUserAnimalHandlerTests
    {
        private UpdateUserAnimalHandler _handler;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<ILogger<UpdateUserAnimalHandler>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockLogger = new Mock<ILogger<UpdateUserAnimalHandler>>();
            _handler = new UpdateUserAnimalHandler(_mockUserRepository.Object, _mockLogger.Object);
        }

        [Test]
        public async Task Handle_ValidInput_ReturnsUnit()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var oldAnimalId = Guid.NewGuid();
            var newAnimalId = Guid.NewGuid();

            var command = new UpdateUserAnimalCommand(userId, oldAnimalId, newAnimalId);

            _mockUserRepository.Setup(x => x.UpdateUserAnimal(userId, oldAnimalId, newAnimalId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.AreEqual(Unit.Value, result);
            _mockUserRepository.Verify(
                x => x.UpdateUserAnimal(userId, oldAnimalId, newAnimalId),
                Times.Once);
        }
    }
}
