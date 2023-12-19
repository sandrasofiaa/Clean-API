using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using API.Controllers.DogsController;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Queries.Dogs.GetAll; // Make sure to include the correct namespace for GetAllDogsQuery
using Domain.Models; // Make sure to include the correct namespace for Dog model
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Test.TestHelpers;
using AutoFixture.NUnit3;
using Microsoft.AspNetCore.Http;

namespace Test.ApiTests
{
    public class DogControllerTest
    {
        [Test, CustomAutoData]
        public async Task GetAllDogs_ShouldReturnOk(
            [Frozen] Mock<IMediator> mediatorMock,
            List<Dog> expectedDogs)
        {
            // Arrange
            var mediator = mediatorMock.Object;
            mediatorMock.Setup(m => m.Send(It.IsAny<GetAllDogsQuery>(), default)).ReturnsAsync(expectedDogs);

            var controller = new DogController(mediator);

            // Using ControllerContext from ControllerBase
            var controllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new Microsoft.AspNetCore.Routing.RouteData(),
                ActionDescriptor = new ControllerActionDescriptor()
            };
            controller.ControllerContext = controllerContext;

            // Act
            var actionResult = await controller.GetAllDogs();
            var result = actionResult as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            var dogs = result.Value as List<Dog>;

            Assert.IsNotNull(dogs);
            Assert.That(dogs, Is.EqualTo(expectedDogs));
        }
    }
}