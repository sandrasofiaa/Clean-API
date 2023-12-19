using Application.Common.Birds;
using Application.Commands.Birds;
using Application.Commands.Birds.DeleteBird;
using Application.Dtos;
using Application.Queries.Birds.GetAll;
using Application.Queries.Birds.GetByColor;
using Application.Queries.Birds.GetById;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.BirdController
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BirdController : ControllerBase
    {
        internal readonly IMediator _mediator;
        public BirdController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all Birds from database
        [HttpGet]
        [Route("getAllBirds")]
        public async Task<IActionResult> GetAllBirds()
        {
            return Ok(await _mediator.Send(new GetAllBirdsQuery()));
        }

        //Get a Birds by Id
        [HttpGet]
        [Route("getBirdById/{birdId}")]
        public async Task<IActionResult> GetBirdById(Guid birdId)
        {
            return Ok(await _mediator.Send(new GetBirdByIdQuery(birdId)));
        }


        // Create a new bird 
        [HttpPost]
        [Route("addNewBird")]
        public async Task<IActionResult> AddBird([FromBody] BirdDto newBird)
        {
            try
            {
                return Ok(await _mediator.Send(new AddBirdCommand(newBird)));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update a specific Bird
        [HttpPut]
        [Route("updateBird/{updatedBirdId}")]
        public async Task<IActionResult> UpdateBird([FromBody] BirdDto updatedBird, Guid updatedBirdId)
        {
            var existingBird = await _mediator.Send(new GetBirdByIdQuery(updatedBirdId));

            if (existingBird == null)
            {
                return NotFound();
            }

            var updateCommand = new UpdateBirdByIdCommand(updatedBird, updatedBirdId);
            var updatedBirdResult = await _mediator.Send(updateCommand);

            if (updatedBirdResult == null)
            {
                return BadRequest();
            }

            return Ok(updatedBirdResult);
        }

        // Delete Bird

        [HttpDelete]
        [Route("{DeleteBirdId}")]
        public async Task<IActionResult> DeleteBird(Guid DeleteBirdId)
        {
            var deleteCommand = new DeleteBirdByIdCommand(DeleteBirdId);
            var deletionResult = await _mediator.Send(deleteCommand);

            if (deletionResult)
            {
                return Ok("Bird deleted successfully");
            }
            else
            {
                return BadRequest("Failed to delete Bird");
            }
        }

        [HttpGet("color/{color}")]
        public async Task<ActionResult<List<Bird>>> GetBirdsByColor(string color)
        {
            var query = new GetBirdByColorQuery (color);
            var birds = await _mediator.Send(query);

            return Ok(birds);
        }
    }
}