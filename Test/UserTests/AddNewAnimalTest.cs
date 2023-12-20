using Application.CommandHandlers.Users;
using Application.Commands.Users;
using AutoFixture.NUnit3;
using Domain.Dtos;
using Domain.Models;
using Infrastructure.Interface;
using Moq;
using Test.TestHelpers;

namespace Test.UserTests
{
    [TestFixture]
    public class AddNewAnimalTest
    {
        private AddNewAnimalCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            var mockAnimalUserRepository = new Mock<IUserRepository>();
            mockAnimalUserRepository.Setup(x => x.AddUserAnimalAsync(It.IsAny<UserAnimal>()))
                .ReturnsAsync(true);
            _handler = new AddNewAnimalCommandHandler(mockAnimalUserRepository.Object);
        }

        [Test]
        [CustomAutoData]
        public async Task When_HandleWithValidAnimal_Then_AnimalIsAddedToUser([Frozen] UserAnimalDto NewAnimalToUser)
        {
            // Arrange
            var command = new AddNewAnimalCommand(NewAnimalToUser);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.True);
        }
    }
}