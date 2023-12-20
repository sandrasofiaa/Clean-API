using Application.Animals.Queries.Cats.GetAll;
using Application.Queries.Cats.GetAll;
using AutoFixture.NUnit3;
using Domain.Models;
using Domain.Models.Animal;
using Infrastructure.Interface;
using Microsoft.Extensions.Logging;
using Moq;
using Test.TestHelpers;

namespace Test.CatTests.QueryTest
{
    [TestFixture]
    public class GetAllCatsTests
    {
        private Mock<IAnimalRepository> _animalRepositoryMock;
        private Mock<ILogger<GetAllCatsQueryHandler>> _loggerMock;

        private GetAllCatsQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _animalRepositoryMock = new Mock<IAnimalRepository>();
            _loggerMock = new Mock<ILogger<GetAllCatsQueryHandler>>();
            _handler = new GetAllCatsQueryHandler(_animalRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        [CustomAutoData]
        public async Task Handle_GetAllCats_ReturnsValidCatList([Frozen] List<Cat> cats)
        {
            // Arrange
            var animalModels = cats.Cast<AnimalModel>().ToList();
            _animalRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(animalModels);

            var query = new GetAllCatsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<Cat>>());
            Assert.That(result, Is.Not.Empty);
        }
    }
}