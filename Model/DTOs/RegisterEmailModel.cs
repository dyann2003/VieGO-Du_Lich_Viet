using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class RegisterEmailModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
