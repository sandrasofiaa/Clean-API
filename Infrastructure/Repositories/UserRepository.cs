using Domain.Data;
using Domain.Models;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Infrastructure.Repositories.AnimalRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly AnimalDbContext _animalDbContext;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(AnimalDbContext animalDbContext, ILogger<UserRepository> logger)
        {
            _animalDbContext = animalDbContext;
            _logger = logger;
        }

        public async Task<List<UserAnimal>> GetAllUsersWithAnimals()
        {
            try
            {
                _logger.LogInformation("Fetching all users with animals from the database...");

                List<UserAnimal> allUsersWithAnimalsFromDatabase = _animalDbContext.UserAnimals.ToList();

                _logger.LogInformation("Successfully fetched all users with animals.");

                return await Task.FromResult(allUsersWithAnimalsFromDatabase);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting all users with animals from the database", ex);
                throw new Exception("An error occurred while getting all users with animals from the database", ex);
            }
        }

        public async Task RegisterUser(User user, string password)
        {
            try
            {
                _logger.LogInformation("Registering a new user...");

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                user.UserPassword = hashedPassword;

                _animalDbContext.Users.Add(user);
                await _animalDbContext.SaveChangesAsync();

                _logger.LogInformation($"User {user.UserName} registered successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while registering a new user", ex);
                throw new Exception("An error occurred while registering a new user", ex);
            }
        }

        //public async Task<User> GetUserByUsername(string username)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Fetching user by username: {username}");

        //        var user = await _animalDbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);

        //        _logger.LogInformation(user != null ? $"User '{username}' found." : $"User '{username}' not found.");

        //        return user;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"An error occurred while getting user by username: {username}", ex);
        //        throw new Exception($"An error occurred while getting user by username: {username}", ex);
        //    }
        //}


        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            try
            {
                var user = await _animalDbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);

                if (user != null)
                {
                    // Jämför det hashade lösenordet i databasen med det inmatade lösenordet
                    bool passwordMatch = BCrypt.Net.BCrypt.Verify(password, user.UserPassword);

                    if (passwordMatch)
                    {
                        return user;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while authenticating user {username}");
                throw new Exception($"An error occurred while authenticating user {username}", ex);
            }
        }

        public async Task<bool> AddUserAnimalAsync(UserAnimal userAnimal)
        {
            try
            {
                _logger.LogInformation("Adding user animal...");

                var user = await _animalDbContext.Users.FirstOrDefaultAsync(u => u.Id == userAnimal.UserId);
                var animal = await _animalDbContext.AnimalModels.FirstOrDefaultAsync(a => a.AnimalId == userAnimal.AnimalId);

                if (user != null && animal != null)
                {
                    userAnimal.UserName = user.UserName;
                    userAnimal.Name = animal.Name;

                    _animalDbContext.UserAnimals.Add(userAnimal);
                    await _animalDbContext.SaveChangesAsync();

                    _logger.LogInformation("User animal added successfully.");
                    return true;
                }
                else
                {
                    _logger.LogError("User or animal not found.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding user animal.");
                return false;
            }
        }

        public async Task DeleteAnimalByUser(Guid userId, Guid animalId)
        {
            try
            {
                var userAnimal = await _animalDbContext.UserAnimals
                    .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.AnimalId == animalId);

                if (userAnimal != null)
                {
                    _animalDbContext.UserAnimals.Remove(userAnimal);
                    await _animalDbContext.SaveChangesAsync();
                }
                else
                {
                    _logger.LogError("User animal association not found.");
                    throw new ArgumentException("User animal association not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while deleting an animal from the user");
                throw new Exception("An error occurred while deleting an animal from the user", ex);
            }
        }

        public async Task UpdateUserAnimal(Guid userId, Guid oldAnimalId, Guid newAnimalId)
        {
            try
            {
                var user = await _animalDbContext.Users
                    .Include(u => u.UserAnimals)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (user != null)
                {
                    var oldUserAnimal = user.UserAnimals.FirstOrDefault(ua => ua.AnimalId == oldAnimalId);

                    if (oldUserAnimal != null)
                    {
                        _animalDbContext.UserAnimals.Remove(oldUserAnimal);

                        var newAnimal = await _animalDbContext.AnimalModels.FirstOrDefaultAsync(a => a.AnimalId == newAnimalId);

                        if (newAnimal != null)
                        {
                            var newUserAnimal = new UserAnimal
                            {
                                UserId = userId,
                                UserName = user.UserName,
                                AnimalId = newAnimalId,
                                Name = newAnimal.Name, 
                            };

                            _animalDbContext.UserAnimals.Add(newUserAnimal);
                            await _animalDbContext.SaveChangesAsync();
                        }
                        else
                        {
                            throw new ArgumentException("New animal not found.");
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Old animal ID not found for the user.");
                    }
                }
                else
                {
                    throw new ArgumentException("User not found in the database.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating user's animal.", ex);
            }
        }

    }
}