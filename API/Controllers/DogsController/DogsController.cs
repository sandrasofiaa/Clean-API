using Application.Commands.Dogs;
using Application.Commands.Dogs.DeleteDog;
using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Application.Queries.Dogs.GetAll;
using Application.Queries.Dogs.GetById;
using Application.Queries.Dogs.GetDogBreedAndWeight;
using Application.Validators.DogValidator;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.DogsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogController : ControllerBase
    {
        internal readonly IMediator _mediator;
        public DogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all dogs from database
        [HttpGet]
        [Route("getAllDogs")]
        public async Task<IActionResult> GetAllDogs()
        {
            return Ok(await _mediator.Send(new GetAllDogsQuery()));
            //return Ok("GET ALL DOGS");
        }

        // Get a dog by Id
        [HttpGet]
        [Route("getDogById/{dogId}")]
        public async Task<IActionResult> GetDogById(Guid dogId)
        {
            return Ok(await _mediator.Send(new GetDogByIdQuery(dogId)));
        }

        //// Create a new dog 
        [HttpPost]
        [Route("addNewDog")]
        public async Task<IActionResult> AddDog([FromBody] DogDto newDog)
        {
            return Ok(await _mediator.Send(new AddDogCommand(newDog)));
        }


        // Update a specific dog
        [HttpPut]
        [Route("updateDog/{updatedDogId}")]
        public async Task<IActionResult> UpdateDog([FromBody] DogDto updatedDog, Guid updatedDogId)
        {
            // Validate the DogDto using FluentValidation
            var dogValidator = new DogValidator(); // Assuming DogValidator is your FluentValidation validator for DogDto
            var validationResult = await dogValidator.ValidateAsync(updatedDog);

            if (!validationResult.IsValid)
            {
                // If validation fails, return a BadRequest with the validation errors
                return BadRequest(validationResult.Errors);
            }

            var command = new UpdateDogByIdCommand(updatedDog, updatedDogId);

            // Send the command via MediatR and let the handler handle the logic
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    


    // IMPLEMENT DELETE !!!

    [HttpDelete]
        [Route("{DeleteDogId}")]
        public async Task<IActionResult> DeleteDog(Guid DeleteDogId)
        {
            var deleteCommand = new DeleteDogByIdCommand(DeleteDogId);
            var deletionResult = await _mediator.Send(deleteCommand);

            if (deletionResult)
            {
                return Ok("Dog deleted successfully");
            }
            else
            {
                return BadRequest("Failed to delete dog");
            }
        }

        [HttpGet("DogbyBreedAndWeight")]
        public async Task<ActionResult<List<Dog>>> GetDogsByBreedAndWeight([FromQuery] GetDogByBreedAndWeightQuery query)
        {
            var dogs = await _mediator.Send(query);
            return Ok(dogs);
        }
    
    }
}