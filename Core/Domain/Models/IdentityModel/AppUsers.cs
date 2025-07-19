using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.IdentityModel
{
    public class AppUsers : IdentityUser
    {
        public string DisplayName { get; set; } = default!;
        public ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public ICollection<Progress> Progresses { get; set; } = new HashSet<Progress>();
        public ICollection<Quiz> CreatedQuizzes { get; set; } = new HashSet<Quiz>();

    }
}
