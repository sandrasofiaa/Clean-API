using NUnit.Framework;
using Application.Commands.Dogs.DeleteDog;
using System;
using Domain.Models;
using Infrastructure.Database;

namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class DeleteDogTests
    {
        private DeleteDogByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new DeleteDogByIdCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task GivenValidDogId_DeleteDog_ReturnsTrue()
        {
            // Arrange
            var dogToDelete = new Dog { Id = Guid.NewGuid() };
            _mockDatabase.Dogs.Add(dogToDelete);

            var deleteCommand = new DeleteDogByIdCommand(dogToDelete.Id);

            // Act
            bool result = await _handler.Handle(deleteCommand, CancellationToken.None);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNull(_mockDatabase.Dogs.FirstOrDefault(dog => dog.Id == dogToDelete.Id));
        }

        [Test]
        public async Task GivenInvalidDogId_DeleteDog_ReturnsFalse()
        {
            // Arrange - No dog added to the mock database

            var deleteCommand = new DeleteDogByIdCommand(Guid.NewGuid());

            // Act
            bool result = await _handler.Handle(deleteCommand, CancellationToken.None);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
