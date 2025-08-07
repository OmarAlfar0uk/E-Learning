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
    public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.ToTable("Quizzes");

            builder.Property(q => q.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(q => q.Module)
                .WithMany(m => m.Quizzes)
                .HasForeignKey(q => q.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);

             builder.HasOne(q => q.CreatedBy)
                .WithMany() 
                .HasForeignKey(q => q.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
