using Infrastructure.Database;
using Application.Commands.Birds.DeleteBird;

namespace Test.BirdTests.CommandTest
{
    [TestFixture]
    public class DeleteBirdTest
    {
        private DeleteBirdByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _handler = new DeleteBirdByIdCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task DeleteSpecificBirdById_ReturnsTrue()
        {
            // Arrange
            var specificBirdId = new Guid("12345678-1234-5678-1234-567812345677");
            var command = new DeleteBirdByIdCommand(specificBirdId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result); // Ensure that the bird was deleted and returns true
            Assert.IsFalse(_mockDatabase.Birds.Any(b => b.Id == specificBirdId)); // Ensure the bird was removed from the mock database
        }


        [Test]
        public async Task DeleteBirdById_NonExistingId_ReturnsFalse()
        {
            // Arrange
            var nonExistingBirdId = new Guid("11111111-1111-1111-1111-111111111111"); // Use a Guid that doesn't exist in your mock data
            var command = new DeleteBirdByIdCommand(nonExistingBirdId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result); // Ensure that no bird was deleted and returns false
        }

    }
}