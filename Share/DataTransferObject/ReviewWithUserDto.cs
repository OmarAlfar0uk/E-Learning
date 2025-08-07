using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.DataTransferObject
{
    public class ReviewWithUserDto
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string Username { get; set; }
        public string? UserImage { get; set; }
    }
}
