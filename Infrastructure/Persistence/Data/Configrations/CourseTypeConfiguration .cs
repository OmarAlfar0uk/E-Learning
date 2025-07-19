using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configrations
{
    public class CourseTypeConfiguration : IEntityTypeConfiguration<CourseType>
    {
        public void Configure(EntityTypeBuilder<CourseType> builder)
        {
            builder.ToTable("CourseTypes");

            builder.Property(ct => ct.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(ct => ct.Courses)
                .WithOne(c => c.CourseType)
                .HasForeignKey(c => c.TypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
