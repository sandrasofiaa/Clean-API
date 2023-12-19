using Application.CommandHandlers.Users;
using Application.Commands.Users;
using Domain.Dtos;
using Domain.Models;
using Infrastructure.Interface;
using Moq;

namespace Test.Repository.User.AddNewAnimalTest
{
    [TestFixture]
    public class AddNewAnimalTest
    {
        [TestFixture]
        public class AddNewAnimalCommandHandlerTests
        {
            [Test]
            public async Task Handle_AddNewAnimal_ReturnsTrue()
            {
                // Arrange
                var userRepositoryMock = new Mock<IUserRepository>();
                var handler = new AddNewAnimalCommandHandler(userRepositoryMock.Object);

                var newAnimalDto = new UserAnimalDto
                {
                    UserId = Guid.NewGuid(),
                    AnimalId = Guid.NewGuid()
                };

                var command = new AddNewAnimalCommand(newAnimalDto);

                // Act
                userRepositoryMock
                    .Setup(repo => repo.AddUserAnimalAsync(It.IsAny<UserAnimal>()))
                    .ReturnsAsync(true);

                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.IsTrue(result);
                userRepositoryMock.Verify(repo => repo.AddUserAnimalAsync(It.IsAny<UserAnimal>()), Times.Once);
            }

            [Test]
            public async Task Handle_AddNewAnimal_ReturnsFalseOnException()
            {
                // Arrange
                var userRepositoryMock = new Mock<IUserRepository>();
                var handler = new AddNewAnimalCommandHandler(userRepositoryMock.Object);

                var newAnimalDto = new UserAnimalDto
                {
                    UserId = Guid.NewGuid(),
                    AnimalId = Guid.NewGuid()
                };

                var command = new AddNewAnimalCommand(newAnimalDto);

                // Act
                userRepositoryMock
                    .Setup(repo => repo.AddUserAnimalAsync(It.IsAny<UserAnimal>()))
                    .ThrowsAsync(new System.Exception("Some exception message"));

                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.IsFalse(result);
                userRepositoryMock.Verify(repo => repo.AddUserAnimalAsync(It.IsAny<UserAnimal>()), Times.Once);
            }
        }
    }
}