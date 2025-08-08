using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Excptions
{
    public sealed class CourseNotFoundException(int id) : NotFoundException($"Product With Id = {id} Is Not Found")
    {
    }
}
