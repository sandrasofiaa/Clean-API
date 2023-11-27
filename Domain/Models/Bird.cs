using Domain.Models.Animal;

namespace Domain.Models
{
    public class Bird : AnimalModel
    {
        public string screams()
        {
            return "This animal screams";
        }

        public bool CanFly { get; set; }
    }
}