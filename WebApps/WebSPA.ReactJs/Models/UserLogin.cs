using System.ComponentModel.DataAnnotations;

namespace WebSPA.ReactJs.Models
{
    public class UserLogin
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}
