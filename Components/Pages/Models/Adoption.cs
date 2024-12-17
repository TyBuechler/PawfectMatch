using PawfectMatch.Components.Pages.Models;
using System;
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
    }
}