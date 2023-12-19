using Application.Commands.Users;
using Application.Dtos;
using AutoFixture.NUnit3;
using Domain.Models; // Lägg till referensen till User-klassen här
using FluentAssertions;
using Infrastructure.Interface;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;
using Test.TestHelpers;

namespace Test.Repository.User.AddNewAnimalTest
{
    [TestFixture]
    public class RegisterUserTests
    {
        [TestFixture]
        public class RegisterUserCommandHandlerTests
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
            public async Task Handle_ValidRegistration_ReturnsUser([Frozen] UserRegistrationDto newUserDto)
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
}


