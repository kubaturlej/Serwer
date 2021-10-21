using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Football.API.DTOs
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public string ConfirmPassword { get; set; }
        public string Nationality { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int RoleId { get; set; } = 1;
    }
}
