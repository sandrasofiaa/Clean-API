namespace Application.Dtos
{
    public class DeleteAnimalFromUserDto
    {
        public Guid UserId { get; set; }
        public Guid AnimalId { get; set; }
    }

}