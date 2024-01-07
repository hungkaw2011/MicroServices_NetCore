using System.ComponentModel.DataAnnotations;

namespace WebSPA.ReactJs.Models
{
    public class UserRegister
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}
