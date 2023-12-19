namespace Application.Dtos
{
    public class UpdateUserAnimalDto
    {
        public Guid UserId { get; set; }
        public Guid OldAnimalId { get; set; }
        public Guid NewAnimalId { get; set; }
    }

}