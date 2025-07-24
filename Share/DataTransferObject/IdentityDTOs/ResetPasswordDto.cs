using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.DataTransferObject.IdentityDTOs
{
       public  class ResetPasswordDto
    {
        [EmailAddress]
        public string Email { get; set; }
        
        public string Token { get; set; }
       
        public string NewPassword { get; set; }
    }
}
