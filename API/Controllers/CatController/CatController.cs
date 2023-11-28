using Application.Commands.Cats.AddCat;
using Application.Commands.Cats.DeleteCat;
using Application.Commands.Cats.UpdateCat;
using Application.Dtos;
using Application.Queries.Cats.GetAll;
using Application.Queries.Cats.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.CatController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatController : ControllerBase
    {
        internal readonly IMediator _mediator;
        public CatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all Cats from database
        [HttpGet]
        [Route("getAllCats")]
        public async Task<IActionResult> GetAllCats()
        {
            return Ok(await _mediator.Send(new GetAllCatsQuery()));
        }

        //Get a Cats by Id
        [HttpGet]
        [Route("getCatById/{catId}")]
        public async Task<IActionResult> GetCatById(Guid catId)
        {
            return Ok(await _mediator.Send(new GetCatByIdQuery(catId)));
        }


        // Create a new Cat 
        [HttpPost]
        [Route("addNewCat")]
        public async Task<IActionResult> AddCat([FromBody] CatDto newCat)
        {
            try
            {
                return Ok(await _mediator.Send(new AddCatCommand(newCat)));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update a specific Cat
        [HttpPut]
        [Route("updateCat/{updatedCatId}")]
        public async Task<IActionResult> UpdateCat([FromBody] CatDto updatedCat, Guid updatedCatId)
        {
            var existingCat = await _mediator.Send(new GetCatByIdQuery(updatedCatId));

            if (existingCat == null)
            {
                return NotFound();
            }

            var updateCommand = new UpdateCatByIdCommand(updatedCat, updatedCatId);
            var updatedCatResult = await _mediator.Send(updateCommand);

            if (updatedCatResult == null)
            {
                return BadRequest();
            }

            return Ok(updatedCatResult);
        }

        // Delete Cat

        [HttpDelete]
        [Route("{DeleteCatId}")]
        public async Task<IActionResult> DeleteCat(Guid DeleteCatId)
        {
            var deleteCommand = new DeleteCatByIdCommand(DeleteCatId);
            var deletionResult = await _mediator.Send(deleteCommand);

            if (deletionResult)
            {
                return Ok("Cat deleted successfully");
            }
            else
            {
                return BadRequest("Failed to delete Cat");
            }
        }
    }
}
