using Application.Commands.Users;
using Application.Dtos;
using Application.Queries.Users;
using Application.Queries.Users.Login;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        internal readonly IMediator _mediator;

        public UserController(IConfiguration configuration, IMediator mediator)
        {
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto newUser)
        {
            return Ok(await _mediator.Send(new RegisterUserCommand(newUser)));
        }

        [HttpPost]
        [Route("GetUsersWithTheirAnimals")]
        public async Task<IActionResult> GetUsersWithTheirAnimals()
        {
            var command = new GetUsersWithAnimalsQuery();
            var usersWithAllTheirAnimals = await _mediator.Send(command);

            return Ok(usersWithAllTheirAnimals);
        }

        private string GenerateJwtToken(UserRegistrationDto loginUser)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim(ClaimTypes.Name, loginUser.Username),
                // Anpassa detta beroende på hur du vill använda Password i tokenet
                // Observera att det normalt inte är rekommenderat att inkludera lösenord i JWT-token
                new Claim("Password", loginUser.Password)
            }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                if (token != null)
                {
                    return tokenHandler.WriteToken(token);
                }
                else
                {
                    // Hantera fall där token-skapandet misslyckades
                    return "Misslyckades med att skapa token.";
                }
            }
            catch (Exception ex)
            {
                // Hantera exception (logga det, kasta det vidare, etc.)
                return $"Misslyckades med att generera token. Exception: {ex.Message}";
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRegistrationDto model)
        {
            var userDto = await _mediator.Send(new LoginUserQuery(model));

            if (userDto == null)
            {
                return Unauthorized("Invalid username or password");
            }

            var token = GenerateJwtToken(model); // Använd 'model' istället för 'userDto'

            return Ok(new { Token = token });
        }

        [HttpPost]
        [Route("addAnimalToUser")]
        public async Task<IActionResult> AddAnimalToUser([FromBody] AddAnimalToUserDto dto)
        {
            try
            {
                // Skapa en UserAnimalDto från UserAnimal-modellen
                var userAnimalDto = new UserAnimalDto
                {
                    UserId = dto.UserId,
                    AnimalId = dto.AnimalId,
                    // Andra relevanta egenskaper här...
                };

                // Skapa en AddNewAnimalCommand med UserAnimalDto
                var command = new AddNewAnimalCommand(userAnimalDto);
                var success = await _mediator.Send(command);

                if (success)
                {
                    return Ok("Animal added to user successfully");
                }
                else
                {
                    return BadRequest("Failed to add animal to user");
                }
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpPost]
        //[Route("addAnimalToUser")]
        //public async Task<IActionResult> AddAnimalToUser([FromBody] AddAnimalToUserDto dto)
        //{
        //    try
        //    {
        //        var command = new AddNewAnimalCommand(dto.UserId, dto.AnimalId);
        //        var success = await _mediator.Send(command);

        //        if (success)
        //        {
        //            return Ok("Animal added to user successfully");
        //        }
        //        else
        //        {
        //            return BadRequest("Failed to add animal to user");
        //        }
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        [HttpDelete]
        [Route("deleteAnimalFromUser")]
        public async Task<IActionResult> DeleteAnimalFromUser([FromBody] DeleteAnimalFromUserDto dto)
        {
            try
            {
                var command = new DeleteAnimalByUserCommand(dto.UserId, dto.AnimalId);
                await _mediator.Send(command);

                return Ok("Animal deleted from user successfully");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("updateUserAnimal")]
        public async Task<IActionResult> UpdateUserAnimal([FromBody] UpdateUserAnimalDto dto)
        {
            var validator = new UpdateUserAnimalDtoValidator();
            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => error.ErrorMessage);
                return BadRequest(errors);
            }

            try
            {
                var command = new UpdateUserAnimalCommand(dto.UserId, dto.OldAnimalId, dto.NewAnimalId);
                await _mediator.Send(command);

                return Ok("User's animal updated successfully");
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException)
                {
                    return NotFound(ex.Message);
                }

                // Print ex.ToString() to get a more detailed error message for debugging
                return StatusCode(500, ex.ToString());
            }
        }
    }
}