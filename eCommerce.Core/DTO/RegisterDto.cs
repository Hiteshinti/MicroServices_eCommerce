using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.DTO
{
    public class RegisterDto
    {
        [Required(ErrorMessage ="Filed is Required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Filed is Required")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Filed is Required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Filed is Required")]
        public GenderOptions Gender { get; set; }  
    }
}
