namespace PawfectMatch.Components.Pages.Models
{ 
        public class dbUser
        {
            public int Id { get; set; } // Primary Key
            public string Username { get; set; } // Username
            public string Password { get; set; } // Hashed Password
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Optional: Timestamp
        }
}

