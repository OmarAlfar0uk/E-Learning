using Domain.Models.IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Quiz : BaseEntity<int>
    {
        public string Title { get; set; } = null!;
        public int ModuleId { get; set; }
        public Module Module { get; set; } = null!;

        public string CreatedById { get; set; }
        public AppUsers CreatedBy { get; set; } = null!;

        public ICollection<QuizQuestion> Questions { get; set; } = new List<QuizQuestion>();


    }
}
