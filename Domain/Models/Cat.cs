using Domain.Models.Animal;

namespace Domain.Models
{
    public class Cat : AnimalModel
    {
        public string Mjau()
        {
            return "This animal says mjau";
        }

        public bool LikesToPlay { get; set; }

        public string Breed { get; set; }

        public int Weight { get; set; }
    }
}