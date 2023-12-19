using Domain.Models.Animal;

namespace Domain.Models
{
    public class UserAnimal
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } // Added UserName property
        public User User { get; set; }

        public Guid AnimalId { get; set; }
        public string Name { get; set; } // Added Name property
        public AnimalModel Animal { get; set; }
    }
}