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
            new Dog { Id = Guid.NewGuid(), Name = "Björn"},
            new Dog { Id = Guid.NewGuid(), Name = "Patrik"},
            new Dog { Id = Guid.NewGuid(), Name = "Alfred"},
            new Dog { Id = new Guid("12345678-1234-5678-1234-567812345678"), Name = "TestDogForUnitTests"}
        };

        public List<Bird> Birds
        {
            get { return allBirds; }
            set { allBirds = value; }
        }

        private static List<Bird> allBirds = new()
        {
            new Bird { Id = Guid.NewGuid(), Name = "FågelnFenix", CanFly = true},
            new Bird { Id = Guid.NewGuid(), Name = "Nemo", CanFly = false},
            new Bird { Id = Guid.NewGuid(), Name = "Sandra", CanFly = true},
            new Bird { Id = new Guid("12345678-1234-5678-1234-567812345677"), Name = "TestBirdForUnitTestsCommand"},
            new Bird { Id = new Guid("12345678-1234-5678-1234-567812345676"), Name = "TestBirdForUnitTestsQueries"}
        };

        public List<Cat> Cats
        {
            get { return allCats; }
            set { allCats = value; }
        }

        private static List<Cat> allCats = new()
        {
            new Cat { Id = Guid.NewGuid(), Name = "Garfield", LikesToPlay = true},
            new Cat { Id = Guid.NewGuid(), Name = "Simba", LikesToPlay = false},
            new Cat { Id = Guid.NewGuid(), Name = "Whiskers", LikesToPlay = true},
            new Cat { Id = new Guid("87654321-4321-8765-4321-876543210123"), Name = "Fluffy"},
            new Cat { Id = new Guid("87654321-4321-8765-4321-876543210124"), Name = "Mittens"}
        };
    }
}
