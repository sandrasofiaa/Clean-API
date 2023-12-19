using Application.Commands.Cats.DeleteCat;
using Application.Commands.Cats.UpdateCat;
using Application.Dtos;
using Application.Queries.Cats.GetAll;
using Application.Queries.Cats.GetById;
using Application.Commands.Cats.AddCat;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Queries.Cats.GetCatsByBreedAndWeight;

namespace API.Controllers.CatsController
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

        [HttpGet]
        [Route("getAllCats")]
        public async Task<IActionResult> GetAllCats()
        {
            return Ok(await _mediator.Send(new GetAllCatsQuery()));
        }

        [HttpGet]
        [Route("getCatById/{catId}")]
        public async Task<IActionResult> GetCatById(Guid catId)
        {
            return Ok(await _mediator.Send(new GetCatByIdQuery(catId)));
        }

        [HttpPost]
        [Route("addNewCat")]
        public async Task<IActionResult> AddCat([FromBody] CatDto newCat)
        {
            return Ok(await _mediator.Send(new AddCatCommand(newCat)));
        }

        [HttpPut]
        [Route("updateCat/{updatedCatId}")]
        public async Task<IActionResult> UpdateCat([FromBody] CatDto updatedCat, Guid updatedCatId)
        {
            var command = new UpdateCatByIdCommand(updatedCat, updatedCatId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

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
                return BadRequest("Failed to delete cat");
            }
        }

        [HttpGet("CatbyBreedAndWeight")]
        public async Task<ActionResult<List<Cat>>> GetCatsByBreedAndWeight([FromQuery] GetCatsByBreedAndWeightQuery query)
        {
            var cats = await _mediator.Send(query);
            return Ok(cats);
        }
    }
}