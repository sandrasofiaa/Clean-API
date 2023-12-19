using Domain.Data;
using Domain.Models;
using Domain.Models.Animal;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories.AnimalRepository
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly AnimalDbContext _animalDbContext;
        private readonly ILogger<AnimalRepository> _logger;

        public AnimalRepository(AnimalDbContext animalDbContext, ILogger<AnimalRepository> logger)
        {
            _animalDbContext = animalDbContext;
            _logger = logger;
        }

        public async Task<List<AnimalModel>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all animals from the database...");
                var animals = await _animalDbContext.AnimalModels.ToListAsync();

                if (animals == null || !animals.Any())
                {
                    _logger.LogInformation("No animals found in the database.");
                    return new List<AnimalModel>();
                }

                return animals;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching all animals: {ex.Message}");
                throw;
            }
        }
        public async Task<AnimalModel> GetByIdAsync(Guid Animalid)
        {
            try
            {
                _logger.LogInformation($"Fetching animal with ID {Animalid}...");
                var animal = await _animalDbContext.AnimalModels.FindAsync(Animalid);

                if (animal == null)
                {
                    _logger.LogInformation($"The animal with ID {Animalid} was not found.");
                    return null;
                }

                return animal;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching animal with ID {Animalid}: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Cat>> GetCatsByWeightBreedAsync(string? breed, int? weight)
        {
            try
            {
                _logger.LogInformation($"Fetching cats by breed: {breed}, and weight: {weight}...");
                var query = _animalDbContext.AnimalModels.OfType<Cat>();

                if (!string.IsNullOrEmpty(breed))
                {
                    query = query.Where(cat => cat.Breed == breed);
                }

                if (weight.HasValue)
                {
                    query = query.Where(cat => cat.Weight >= weight.Value);
                }

                var result = await query.OrderByDescending(cat => cat.Weight).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching cats: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Dog>> GetDogsByWeightBreedAsync(string? breed, int? weight)
        {
            try
            {
                _logger.LogInformation($"Fetching dogs by breed: {breed}, and weight: {weight}...");
                var query = _animalDbContext.AnimalModels.OfType<Dog>();

                if (!string.IsNullOrEmpty(breed))
                {
                    query = query.Where(dog => dog.Breed == breed);
                }

                if (weight.HasValue)
                {
                    query = query.Where(dog => dog.Weight >= weight.Value);
                }

                var result = await query.OrderByDescending(dog => dog.Weight).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching dogs: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Bird>> GetBirdsByColorAsync(string color)
        {
            try
            {
                _logger.LogInformation($"Fetching birds by color: {color}...");
                string upperColor = color.ToUpper();

                var query = _animalDbContext.Birds.Where(b => b.Color.ToUpper() == upperColor);

                query = query.OrderByDescending(b => b.Name)
                             .ThenByDescending(b => b.AnimalId);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching birds: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAsync<T>(Guid animalId) where T : class
        {
            try
            {
                _logger.LogInformation($"Deleting animal with ID: {animalId}...");
                var animalToDelete = await _animalDbContext.Set<T>().FindAsync(animalId) ?? throw new Exception("Could not find Animal");

                _animalDbContext.Set<T>().Remove(animalToDelete);
                await _animalDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting animal: {ex.Message}");
                throw;
            }
        }

        public async Task AddAnimalAsync<T>(T entity) where T : class
        {
            try
            {
                _logger.LogInformation($"Adding a new animal...");
                await _animalDbContext.Set<T>().AddAsync(entity);
                await _animalDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding an animal: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateAnimalAsync(AnimalModel animalModel)
        {
            try
            {
                _logger.LogInformation($"Updating animal with ID: {animalModel.AnimalId}...");
                _animalDbContext.AnimalModels.Update(animalModel);
                await _animalDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating an animal: {ex.Message}");
                throw;
            }
        }
    }
}
