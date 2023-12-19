namespace Application.Dtos
{
    public class GetPetsByBreedAndWeightDto
    {
        public string Name { get; set; }
        public string Breed { get; set; }
        public int? Weight { get; set; }
    }
}