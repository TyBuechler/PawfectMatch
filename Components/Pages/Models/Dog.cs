namespace PawfectMatch.Components.Pages.Models
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        public int Size { get; set; }
        public bool IsAvailable { get; set; }
        public string MedRecords { get; set; }
        public string PrevHistory { get; set; }
        public string ImageUrl { get; set; }
    }
}
