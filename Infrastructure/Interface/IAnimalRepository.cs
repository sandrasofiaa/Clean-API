using Domain.Models;
using Domain.Models.Animal;

namespace Infrastructure.Interface
{
    public interface IAnimalRepository
    {
        Task AddAnimalAsync<T>(T entity) where T : class;
        //Task DeleteAsync(Guid id);
        Task<List<AnimalModel>> GetAllAsync();
        Task<AnimalModel> GetByIdAsync(Guid Animalid);
        Task<List<Cat>> GetCatsByWeightBreedAsync(string? breed, int? weight);
        Task<List<Dog>> GetDogsByWeightBreedAsync(string breed, int? weight);
        Task UpdateAnimalAsync(AnimalModel animalModel);
        Task DeleteAsync<T>(Guid animalId) where T : class;
    }
}