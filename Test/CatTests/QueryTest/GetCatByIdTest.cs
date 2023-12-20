using Application.Animals.Queries.Cats.GetById;
using Application.Queries.Cats.GetById;
using AutoFixture.NUnit3;
using Domain.Models;
using Infrastructure.Interface;
using Moq;
using Test.TestHelpers;

namespace Test.CatTests.QueryTest
{
    [TestFixture]
    public class GetCatByIdQueryHandlerTests
    {

        private GetCatByIdQueryHandler _handler;
        private Mock<IAnimalRepository> _animalRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _animalRepositoryMock = new Mock<IAnimalRepository>();
            _handler = new GetCatByIdQueryHandler(_animalRepositoryMock.Object);
        }

        [Test]
        [CustomAutoData]
        public async Task Given_ValidId_When_GettingCatById_Then_ReturnsCorrectCat([Frozen] Cat cat)
        {
            // Arrange
            _animalRepositoryMock.Setup(x => x.GetByIdAsync(cat.AnimalId)).ReturnsAsync(cat);

            var query = new GetCatByIdQuery(cat.AnimalId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.AnimalId, Is.EqualTo(cat.AnimalId));
        }
    }
}