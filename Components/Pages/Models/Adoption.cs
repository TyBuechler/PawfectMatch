using System;
using PawfectMatch.Components.Pages.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawfectMatch.Components.Pages.Models
{
    public class Adoption
    {
        [Key]
        public int Id { get; set; } // Primary key for the Adoption record

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } // User's email (required field)

        [Required]
        [MaxLength(100)]
        public string DogName { get; set; } // Name of the dog the user wants to adopt

        [Required]
        public int DogId { get; set; } // Foreign key to reference the dog

        [ForeignKey("DogId")]
        public virtual Dog Dog { get; set; } // Navigation property for the Dog entity

        [Required]
        public DateTime SubmissionDate { get; set; } = DateTime.Now; // Date when the form was submitted

        [MaxLength(500)]
        public string AdditionalComments { get; set; } // Optional field for additional comments from the user

        // New Fields
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } // User's full name

        [Required]
        [Phone]
        [MaxLength(20)]
        public string Phone { get; set; } // User's phone number

        [MaxLength(200)]
        public string Address { get; set; } // User's address

        [MaxLength(200)]
        public string Preferences { get; set; } // Dog preferences

        [MaxLength(10)]
        public string HasPetsBefore { get; set; } // Yes/No answer for past pets

        public bool HomeCheck { get; set; } // Agreement to a home check

        [MaxLength(500)]
        public string Message { get; set; } // User's message
    }
}
