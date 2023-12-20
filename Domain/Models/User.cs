using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public ICollection<UserAnimal> UserAnimals { get; set; }
    }
}