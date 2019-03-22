using System.ComponentModel.DataAnnotations;

namespace Nadmin.Dto.Model
{
    public class UserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}