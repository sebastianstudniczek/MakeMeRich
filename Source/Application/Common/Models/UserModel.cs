using System.ComponentModel.DataAnnotations;

namespace MakeMeRich.Application.Common.Models
{
    public class UserModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
