
namespace Application.Dtos
{
    public class AddAnimalToUserDto
    {
        public Guid UserId { get; set; }
        public Guid AnimalId { get; set; }
    }
}