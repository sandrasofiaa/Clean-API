using Domain.Models;

namespace Infrastructure.Database
{
    public class MockDatabase
    {
        public List<Dog> Dogs
        {
            get { return allDogs; }
            set { allDogs = value; }
        }

        private static List<Dog> allDogs = new()
        {
            new Dog { AnimalId = Guid.NewGuid(), Name = "Björn"},
            new Dog { AnimalId = Guid.NewGuid(), Name = "Patrik"},
            new Dog { AnimalId = Guid.NewGuid(), Name = "Alfred"},
            new Dog { AnimalId = new Guid("12345678-1234-5678-1234-567812345678"), Name = "TestDogForUnitTests"}
        };

        public List<Bird> Birds
        {
            get { return allBirds; }
            set { allBirds = value; }
        }

        private static List<Bird> allBirds = new()
        {
            new Bird { AnimalId = Guid.NewGuid(), Name = "FågelnFenix", CanFly = true},
            new Bird { AnimalId = Guid.NewGuid(), Name = "Nemo", CanFly = false},
            new Bird { AnimalId = Guid.NewGuid(), Name = "Janne", CanFly = true},
            new Bird { AnimalId = new Guid("12345678-1234-5678-1234-567812345677"), Name = "TestBirdForUnitTestsCommand"},
            new Bird { AnimalId = new Guid("12345678-1234-5678-1234-567812345676"), Name = "TestBirdForUnitTestsQueries"}
        };

        public List<Cat> Cats
        {
            get { return allCats; }
            set { allCats = value; }
        }

        private static List<Cat> allCats = new()
        {
            new Cat { AnimalId = Guid.NewGuid(), Name = "Garfield", LikesToPlay = true},
            new Cat { AnimalId = Guid.NewGuid(), Name = "Simba", LikesToPlay = false},
            new Cat { AnimalId = Guid.NewGuid(), Name = "Whiskers", LikesToPlay = true},
            new Cat { AnimalId = new Guid("87654321-4321-8765-4321-876543210123"), Name = "Fluffy"},
            new Cat { AnimalId = new Guid("87654321-4321-8765-4321-876543210124"), Name = "Mittens"}
        };

        public List<User> Users
        {
            get { return allUsers; }
            set { allUsers = value; }
        }

        private static List<User> allUsers = new()
        {
            new User { Id = Guid.NewGuid(), UserName = "Nils" },
            new User { Id = Guid.NewGuid(), UserName = "Kalle" },

        };
    }
}