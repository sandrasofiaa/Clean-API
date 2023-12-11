using Application.Commands.Cats.AddCat;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;

namespace Test.CatTests.CommandTest
{
    [TestFixture]
    public class AddCatTest
    {
        private AddCatCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new AddCatCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task GivenValidCatDto_AddCat_ReturnsNewCat()
        {
            // Arrange
            var CatDto = new CatDto { Name = "Nisse", LikesToPlay = true };
            var addCatCommand = new AddCatCommand(CatDto);

            // Act
            Cat addedCat = await _handler.Handle(addCatCommand, CancellationToken.None);

            // Assert
            Assert.IsNotNull(addedCat);
            Assert.That(addedCat.Name, Is.EqualTo(CatDto.Name));
            Assert.That(addedCat.LikesToPlay, Is.EqualTo(CatDto.LikesToPlay));
            Assert.IsTrue(_mockDatabase.Cats.Contains(addedCat));
        }

        [Test]
        public void GivenInvalidCatDto_AddCat_ThrowsArgumentException()
        {
            // Arrange
            var invalidCatDto = new CatDto { Name = "", LikesToPlay = false };
            var addCatCommand = new AddCatCommand(invalidCatDto);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _handler.Handle(addCatCommand, CancellationToken.None));
        }
    }
}