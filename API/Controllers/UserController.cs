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

            var token = GenerateJwtToken(model);

            return Ok(new { Token = token });
        }

        [HttpPost]
        [Route("addAnimalToUser")]
        public async Task<IActionResult> AddAnimalToUser([FromBody] AddAnimalToUserDto dto)
        {

            var userAnimalDto = new UserAnimalDto
            {
                UserId = dto.UserId,
                AnimalId = dto.AnimalId,
            };

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

        [HttpDelete]
        [Route("deleteAnimalFromUser")]
        public async Task<IActionResult> DeleteAnimalFromUser([FromBody] DeleteAnimalFromUserDto dto)
        {
            var command = new DeleteAnimalByUserCommand(dto.UserId, dto.AnimalId);
            await _mediator.Send(command);

            return Ok("Animal deleted from user successfully");
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

            var command = new UpdateUserAnimalCommand(dto.UserId, dto.OldAnimalId, dto.NewAnimalId);
            await _mediator.Send(command);

            return Ok("User's animal updated successfully");
        }
    }
}