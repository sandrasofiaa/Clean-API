using Domain.Models;

namespace Infrastructure.Interface
{
    public interface IUserRepository
    {
        Task<List<UserAnimal>> GetAllUsersWithAnimals();
        Task RegisterUser(User user, string password);
        //Task<User> GetUserByUsername(string username);
        Task<User> AuthenticateUserAsync(string username, string password);
        //Task AddAnimalToUser(Guid userId, Guid animalId);
        Task DeleteAnimalByUser(Guid userId, Guid animalId);
        Task UpdateUserAnimal(Guid userId, Guid oldAnimalId, Guid newAnimalId);

        Task<bool> AddUserAnimalAsync(UserAnimal userAnimal);
    }
}