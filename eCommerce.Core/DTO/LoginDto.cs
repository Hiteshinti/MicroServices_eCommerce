using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.DTO
{
     public  class LoginDto
    {
         [Required (ErrorMessage="Filed is Required")]
         public string? Email { get; set; }
         [Required(ErrorMessage = "Filed is Required")] 
         public string? Password { get; set; }
    }
}
