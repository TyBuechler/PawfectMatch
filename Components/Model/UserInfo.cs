using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Components.Model
{
    public class UserInfo
    {
        [Key]
        public string username { get; set; }
        public string password { get; set; }


    }
}
