using PawfectMatch.Components.Pages.Models;

namespace PawfectMatch.Components.Services
{
    public class DogService
    {
        private List<Dog> dogs = new List<Dog>
    {
        new Dog { Id = 1, Name = "Buddy", Breed = "Golden Retriever", Age = 3, Size = 80, IsAvailable = true, MedRecords = "Vaccinated", PrevHistory = "No issues", ImageUrl = "golden1.jpeg" },
        new Dog { Id = 2, Name = "Charlie", Breed = "Labrador Retriever", Age = 4, Size = 65, IsAvailable = true, MedRecords = "Needs checkup", PrevHistory = "Adopted once", ImageUrl = "labrador1.jpeg" },
        new Dog { Id = 3, Name = "Max", Breed = "German Shepherd", Age = 5, Size = 80, IsAvailable = true, MedRecords = "Healthy", PrevHistory = "Trained", ImageUrl = "germanshepherd1.jpeg" },
        new Dog { Id = 4, Name = "Bella", Breed = "Poodle", Age = 2, Size = 40, IsAvailable = true, MedRecords = "Vaccinated", PrevHistory = "Friendly", ImageUrl = "poodle1.jpeg" },
        new Dog { Id = 5, Name = "Molly", Breed = "Bulldog", Age = 6, Size = 55, IsAvailable = true, MedRecords = "Requires dental care", PrevHistory = "Rescued", ImageUrl = "bulldog1.jpeg" },
        new Dog { Id = 6, Name = "Rocky", Breed = "Beagle", Age = 3, Size = 30, IsAvailable = true, MedRecords = "Healthy", PrevHistory = "Energetic", ImageUrl = "beagle1.jpeg" },
        new Dog { Id = 7, Name = "Lucy", Breed = "Boxer", Age = 4, Size = 75, IsAvailable = true, MedRecords = "Vaccinated", PrevHistory = "Guard dog", ImageUrl = "boxer1.jpeg" },
        new Dog { Id = 8, Name = "Daisy", Breed = "Siberian Husky", Age = 2, Size = 60, IsAvailable = true, MedRecords = "Healthy", PrevHistory = "Needs exercise", ImageUrl = "husky1.jpeg" },
        new Dog { Id = 9, Name = "Bailey", Breed = "Shih Tzu", Age = 5, Size = 20, IsAvailable = true, MedRecords = "Needs eye care", PrevHistory = "Calm nature", ImageUrl = "shihtzu1.jpeg" },
        new Dog { Id = 10, Name = "Luna", Breed = "Golden Retriever", Age = 2, Size = 60, IsAvailable = true, MedRecords = "Vaccinated", PrevHistory = "No issues", ImageUrl = "golden2.jpeg" },
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
