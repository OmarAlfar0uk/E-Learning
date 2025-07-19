using Domain.Models.IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Review :BaseEntity<int>
    {
        public int Rating { get; set; } 
        public string Comment { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public string AppUserId { get; set; }
        public AppUsers AppUser { get; set; } = null!;


    }
}
