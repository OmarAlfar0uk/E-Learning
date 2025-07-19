﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Category :BaseEntity<int>
    {
        public string Name { get; set; } = null!;

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
