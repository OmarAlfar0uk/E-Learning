using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.DataTransferObject
{
    public class CourseDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string Title { get; set; } = default!;

        public string PictureUrl { get; set; } = default!;

        public string PromoVideoUrl { get; set; } = default!;

        public decimal Price { get; set; }

        public string Level { get; set; } = default!;

        public TimeSpan Duration { get; set; }

        public bool IsPublished { get; set; }

        public DateTime? PublishedAt { get; set; }

        public string Language { get; set; } = default!;

        public string TypeName { get; set; } = default!;

        public int CategoryId { get; set; }
     
        public string CategoryName { get; set; } = default!;
    }
}
