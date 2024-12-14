using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Components.Pages.Models
{
    public class User
    {
        [Key]
        public string username { get; set; }
        public string password { get; set; }
    }
}
