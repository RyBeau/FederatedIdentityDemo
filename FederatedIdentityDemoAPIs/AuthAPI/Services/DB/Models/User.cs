using System.ComponentModel.DataAnnotations.Schema;

namespace AuthAPI.Services.DB.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        [Column("role_id")]
        public int Role { get; set; }
    }
}
