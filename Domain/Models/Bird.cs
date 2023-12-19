using Domain.Models.Animal;

namespace Domain.Models
{
    public class Bird : AnimalModel
    {
        public string Screams()
        {
            return "This animal screams";
        }

        public bool CanFly { get; set; }

        public string Color { get; set; }
    }
}