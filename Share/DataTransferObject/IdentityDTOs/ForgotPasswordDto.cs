using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.DataTransferObject.IdentityDTOs
{
    public class ForgotPasswordDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string ClientAppUrl { get; set; }
    }
}
