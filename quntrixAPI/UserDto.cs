using System.ComponentModel.DataAnnotations;

namespace quntrixAPI
{
    public class UserDto
    {   
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }= string.Empty;
        public string Password { get; set; }= string.Empty;
    }
}
