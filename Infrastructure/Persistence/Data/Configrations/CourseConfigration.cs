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
    public class CourseConfigration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");


            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);


            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(100);
              

            builder.Property(c => c.Description)
                .IsRequired()
                .HasColumnType("text");

            builder.Property(c => c.PictureUrl)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(c => c.Price)
                .IsRequired()
                .HasColumnType("numeric(10,2)");

            builder.Property(c => c.IsPublished)
                .HasDefaultValue(false);

            builder.Property(c => c.PublishedAt)
                .IsRequired(false);

            builder.Property(c => c.Language)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Level)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.PromoVideoUrl)
                .HasMaxLength(500);

            builder.Property(c => c.Duration)
                .HasConversion(
                    v => v.ToString(),
                    v => TimeSpan.Parse(v)
                );


            builder.HasOne(c => c.CourseType)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.TypeId)
                .OnDelete(DeleteBehavior.Restrict);


            
            
            builder.HasOne(c => c.Category)
                .WithMany(cat => cat.Courses)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }
}