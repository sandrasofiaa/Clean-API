//using Application.Animals.Queries.Dogs.GetAll;
//using Application.Queries.Dogs.GetAll;
//using Domain.Models;
//using Domain.Models.Animal;
//using Infrastructure.Interface;
//using Microsoft.Extensions.Logging;
//using Moq;

//namespace YourNamespace.Tests
//{
//    [TestFixture]
//    public class GetAllDogsQueryHandlerTests
//    {
//        [Test]
//        public async Task Handle_Returns_All_Dogs()
//        {
//            // Arrange
//            var mockAnimalRepository = new Mock<IAnimalRepository>();
//            var mockLogger = new Mock<ILogger<GetAllDogsQueryHandler>>();

//            var dogs = new List<Dog>
//            {
//                new Dog { /* Dog properties */ },
//                new Dog { /* Dog properties */ },

//            };

//            mockAnimalRepository.Setup(repo => repo.GetAllAsync())
//                                .ReturnsAsync(new List<AnimalModel>(dogs));

//            var handler = new GetAllDogsQueryHandler(mockAnimalRepository.Object, mockLogger.Object);
//            var query = new GetAllDogsQuery();

//            // Act
//            var result = await handler.Handle(query, CancellationToken.None);

//            // Assert
//            Assert.NotNull(result);
//            Assert.AreEqual(dogs.Count, result.Count);
//        }

//        [Test]
//        public async Task Handle_Returns_Empty_List_When_No_Dogs_Found()
//        {
//            // Arrange
//            var mockAnimalRepository = new Mock<IAnimalRepository>();
//            var mockLogger = new Mock<ILogger<GetAllDogsQueryHandler>>();

//            mockAnimalRepository.Setup(repo => repo.GetAllAsync())
//                                .ReturnsAsync(new List<AnimalModel>());

//            var handler = new GetAllDogsQueryHandler(mockAnimalRepository.Object, mockLogger.Object);
//            var query = new GetAllDogsQuery();

//            // Act
//            var result = await handler.Handle(query, CancellationToken.None);

//            // Assert
//            Assert.NotNull(result);
//            Assert.IsEmpty(result);
//            // Add more assertions based on your specific logic
//        }
//    }
//}