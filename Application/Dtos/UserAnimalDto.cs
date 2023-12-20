namespace Domain.Dtos
{
    public class UserAnimalDto
    {
        public Guid UserId { get; set; }
        public Guid AnimalId { get; set; }
        public string UserName { get; set; }
        public string AnimalName { get; set; }
    }
}