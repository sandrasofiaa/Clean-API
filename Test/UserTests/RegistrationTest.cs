using Application.Commands.Users;
using Application.Dtos;
using AutoFixture.NUnit3;
using FluentAssertions;
using Infrastructure.Interface;
using Moq;
using Test.TestHelpers;

namespace Test.UserTests
{
    [TestFixture]
    public class RegisterUserTests
    {
        private RegisterUserCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            _handler = new RegisterUserCommandHandler(mockUserRepository.Object);
        }

        [Test]
        [CustomAutoData]
        public async Task When_HandleValidRegistration_Then_ReturnsUser([Frozen] UserRegistrationDto newUserDto)
        {
            // Arrange
            var command = new RegisterUserCommand(newUserDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.UserName.Should().Be(command.NewUser.Username);
        }
    }
}


