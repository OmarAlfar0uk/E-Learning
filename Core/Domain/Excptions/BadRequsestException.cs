using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Excptions
{
    public sealed class BadRequsestException(List<string> Errors) : Exception("Validation Failed")
    {
        public List<string> Errors { get; } = Errors;
    }
}
