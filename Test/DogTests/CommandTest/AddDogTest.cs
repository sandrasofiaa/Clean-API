using Application.Commands.Dogs;
using Application.Dtos;
using Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class AddDogTests
    {
        [Test]
        public async Task Handle_AddsNewDogToDatabase()
        {
            // Arrange
            var mockDatabase = new MockDatabase();
            var dogHandler = new AddDogCommandHandler(mockDatabase);

            var expectedDogName = "Rex";
            var addDogCommand = new AddDogCommand(new DogDto { Name = expectedDogName });

            // Act
            var newlyAddedDog = await dogHandler.Handle(addDogCommand, CancellationToken.None);

            // Assert
            Assert.IsNotNull(newlyAddedDog);
            Assert.That(newlyAddedDog.Name, Is.EqualTo(expectedDogName));

            // Check if the newly added dog exists in the mock database
            var isDogAddedToDatabase = mockDatabase.Dogs.Any(d => d.Id == newlyAddedDog.Id && d.Name == expectedDogName);
            Assert.IsTrue(isDogAddedToDatabase);
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
