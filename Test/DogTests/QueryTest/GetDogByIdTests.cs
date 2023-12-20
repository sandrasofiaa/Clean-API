using Application.Animals.Queries.Dogs.GetById;
using Application.Queries.Dogs.GetById;
using AutoFixture.NUnit3;
using Domain.Models;
using Infrastructure.Interface;
using Moq;
using Test.TestHelpers;

namespace Test.DogTests.QueryTest
{
    [TestFixture]
    public class GetDogByIdQueryHandlerTests
    {

        private GetDogByIdQueryHandler _handler;
        private Mock<IAnimalRepository> _animalRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _animalRepositoryMock = new Mock<IAnimalRepository>();
            _handler = new GetDogByIdQueryHandler(_animalRepositoryMock.Object);
        }

        [Test]
        [CustomAutoData]
        public async Task Given_ValidId_When_GettingDogById_Then_ReturnsCorrectDog([Frozen] Dog dog)
        {
            // Arrange
            _animalRepositoryMock.Setup(x => x.GetByIdAsync(dog.AnimalId)).ReturnsAsync(dog);

            var query = new GetDogByIdQuery(dog.AnimalId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.AnimalId, Is.EqualTo(dog.AnimalId));
        }
    }
}