using PawfectMatch.Components.Pages.Models;

namespace PawfectMatch.Components.Services
{
    public class DogService
    {
        private List<Dog> dogs = new List<Dog>
    {
        new Dog { Id = 1, Name = "Buddy", Breed = "Golden Retriever", Age = 3, ImageUrl = "golden1.jpeg" },
        new Dog { Id = 2, Name = "Luna", Breed = "Golden Retriever", Age = 2, ImageUrl = "golden2.jpg" },
        // Add more dogs after first stuff
    };

        public Task<List<Dog>> GetDogsAsync()
        {
            return Task.FromResult(dogs);
        }

        public Task<Dog> GetDogByIdAsync(int id)
        {
            var dog = dogs.FirstOrDefault(d => d.Id == id);
            return Task.FromResult(dog);
        }
    }

}
