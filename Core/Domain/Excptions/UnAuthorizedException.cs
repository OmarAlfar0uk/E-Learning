using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Excptions
{
    public sealed class UnAuthorizedException(string message ="InValed Email Or Password") : Exception(message)
    {
    }
}
