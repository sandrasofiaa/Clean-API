//using Application.Commands.Dogs.UpdateDog;
//using Application.Queries.Dogs.GetDogBreedAndWeight;
//using Application.Queries.Dogs.GetDogByBreedAndWeight;
//using Domain.Data;
//using Domain.Models;
//using FluentValidation;
//using Infrastructure.Interface;
//using Moq;

//namespace Test.DogTests.QueryTest
//{
//    [TestFixture]
//    public class GetDogBreedAndWeightQueryHandlerTests
//    {

//            [Test]
//            public async Task Handle_ValidRequest_ReturnsListOfDogs()
//            {
//                // Arrange
//                var breed = "Labrador";
//                var weight = 30;

//                var mockAnimalRepository = new Mock<IAnimalRepository>();
//                mockAnimalRepository.Setup(repo => repo.GetDogsByWeightBreedAsync(breed, weight))
//                                    .ReturnsAsync(new List<Dog>
//                                    {
//                                    new Dog { Breed = breed, Weight = weight },
//                                        // Add more mocked dogs if needed for your test cases
//                                    });

//                var query = new GetDogByBreedAndWeightQuery { Breed = breed, Weight = weight };
//                var handler = new GetDogBreedAndWeightQueryHandler(null, mockAnimalRepository.Object);

//                // Act
//                var result = await handler.Handle(query, CancellationToken.None);

//                // Assert
//                Assert.IsNotNull(result);
//                Assert.IsInstanceOf<List<Dog>>(result);
//                // Add more specific assertions based on your expected behavior
//            }

//        [Test]
//        public async Task Handle_InvalidRequest_ThrowsValidationException()
//        {
//            // Arrange
//            var invalidBreed = ""; // Empty breed to trigger validation failure
//            var weight = 30;

//            var query = new GetDogByBreedAndWeightQuery { Breed = invalidBreed, Weight = weight };
//            var handler = new GetDogBreedAndWeightQueryHandler(null, Mock.Of<IAnimalRepository>());

//            // Act & Assert
//            Assert.ThrowsAsync<ValidationException>(async () => await handler.Handle(query, CancellationToken.None));
//        }

//    }
//}
