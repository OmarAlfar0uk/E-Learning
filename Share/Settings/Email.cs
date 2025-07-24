using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Share.Settings
{
    public class Email
    {
        public string To { get; set; }
        public string subject { get; set; }
        public string Body { get; set; }
    }
}
