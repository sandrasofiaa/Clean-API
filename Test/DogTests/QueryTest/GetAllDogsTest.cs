using Application.Animals.Queries.Dogs.GetAll;
using Application.Queries.Dogs.GetAll;
using AutoFixture.NUnit3;
using Domain.Models;
using Domain.Models.Animal;
using Infrastructure.Interface;
using Microsoft.Extensions.Logging; // Import the namespace for ILogger
using Moq;
using Test.TestHelpers;

namespace Test.DogTests.QueryTest
{
    [TestFixture]
    public class GetAllDogsTests
    {
        private Mock<IAnimalRepository> _animalRepositoryMock;
        private Mock<ILogger<GetAllDogsQueryHandler>> _loggerMock; // Logger mock

        private GetAllDogsQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _animalRepositoryMock = new Mock<IAnimalRepository>();
            _loggerMock = new Mock<ILogger<GetAllDogsQueryHandler>>(); // Initialize logger mock
            _handler = new GetAllDogsQueryHandler(_animalRepositoryMock.Object, _loggerMock.Object); // Pass logger mock
        }

        [Test]
        [CustomAutoData]
        public async Task Handle_GetAllDogs_ReturnsValidDogList([Frozen] List<Dog> dogs)
        {
            // Arrange
            var animalModels = dogs.Cast<AnimalModel>().ToList(); // Assuming Dog inherits from AnimalModel
            _animalRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(animalModels);

            var query = new GetAllDogsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<Dog>>());
            Assert.That(result, Is.Not.Empty);
        }
    }
}
