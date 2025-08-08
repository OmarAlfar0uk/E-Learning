using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Excptions
{
    public sealed class DeliveryMethodNotFoundException(int id) : NotFoundException($"No DeliveryMethod Find By Id {id}")
    {
    }
}
