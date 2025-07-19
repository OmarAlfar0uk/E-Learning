using Domain.Models.IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Course : BaseEntity<int>
    {
        #region Prop for Course
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string PictureUrl { get; set; } = null!;

        public string PromoVideoUrl { get; set; } = null!;

        public decimal Price { get; set; }

        public string Level { get; set; } = null!;

        public TimeSpan Duration { get; set; }

        public bool IsPublished { get; set; } 

        public DateTime? PublishedAt { get; set; }

        public string Language { get; set; } = null!;



        #endregion

        public string InstructorId { get; set; } = null!;
        public AppUsers Instructor { get; set; } = null!;

        public int TypeId { get; set; }
        public CourseType CourseType { get; set; } = null!;


        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

    }
}
