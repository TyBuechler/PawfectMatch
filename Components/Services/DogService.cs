using Microsoft.EntityFrameworkCore;
using PawfectMatch.Components.Pages.Models;
using PawfectMatch.Data;
using System;

namespace PawfectMatch.Components.Services
{
    public class DogService
    {
        private readonly PawfectMatchContext _context;
        private List<Dog> dogs = new List<Dog>();

        public DogService(PawfectMatchContext context)
        {
            _context = context;
        }

        // Load dogs from the database into the in-memory list
        private async Task LoadDogsAsync()
        {
            dogs = await _context.Dog.ToListAsync();
        }

        public async Task<List<Dog>> GetDogsAsync()
        {
            await LoadDogsAsync();  // Ensure in-memory list is updated
            return dogs;
        }

        public async Task<Dog> GetDogByIdAsync(int id)
        {
            await LoadDogsAsync();
            return dogs.FirstOrDefault(d => d.Id == id);
        }

        // Add a new dog to both the database and in-memory list
        public async Task AddDogAsync(Dog newDog)
        {
            // Add to the database
            _context.Dog.Add(newDog);
            await _context.SaveChangesAsync();

            // Update the in-memory list
            await LoadDogsAsync();
        }
    }
}
