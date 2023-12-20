using Application.Commands.Cats.DeleteCat;
using AutoFixture.NUnit3;
using Domain.Models;
using Infrastructure.Interface;
using Moq;
using Test.TestHelpers;

namespace Test.CatTests.CommandTest
{
    [TestFixture]
    public class DeleteCatByIdCommandHandlerTests
    {
        private Mock<IAnimalRepository> _animalRepositoryMock;
        private DeleteCatByIdCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _animalRepositoryMock = new Mock<IAnimalRepository>();
            _handler = new DeleteCatByIdCommandHandler(_animalRepositoryMock.Object);
        }

        [Test]
        [CustomAutoData]
        public async Task DeleteCatByIdHandler_DeletesCat_ReturnsTrue([Frozen] Cat initialCat)
        {
            // Arrange
            _animalRepositoryMock.Setup(x => x.GetByIdAsync(initialCat.AnimalId)).ReturnsAsync(initialCat);

            // Act
            var command = new DeleteCatByIdCommand(initialCat.AnimalId);
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.True);
        }
    }
}