using Application.Commands.Users;
using Application.Handlers.Users;
using Infrastructure.Interface;
using MediatR;
using Moq;

namespace Test.UserTests
{
    [TestFixture]
    public class DeleteAnimalByUserHandlerTests
    {
        [Test]
        public async Task Handle_DeleteAnimal_Success()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var animalId = Guid.NewGuid();

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock
                .Setup(repo => repo.DeleteAnimalByUser(userId, animalId))
                .Returns(Task.CompletedTask);

            var deleteAnimalByUserHandler = new DeleteAnimalByUserHandler(userRepositoryMock.Object);
            var deleteAnimalCommand = new DeleteAnimalByUserCommand(userId, animalId);

            // Act
            var result = await deleteAnimalByUserHandler.Handle(deleteAnimalCommand, CancellationToken.None);

            // Assert
            userRepositoryMock.Verify(repo => repo.DeleteAnimalByUser(userId, animalId), Times.Once);
            Assert.That(result, Is.EqualTo(Unit.Value));
        }
    }
}