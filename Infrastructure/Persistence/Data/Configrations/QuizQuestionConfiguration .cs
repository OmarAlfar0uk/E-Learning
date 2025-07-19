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
    public class QuizQuestionConfiguration : IEntityTypeConfiguration<QuizQuestion>
    {
        public void Configure(EntityTypeBuilder<QuizQuestion> builder)
        {
            builder.ToTable("QuizQuestions");

            builder.Property(q => q.QuestionText)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(q => q.OptionA).IsRequired().HasMaxLength(200);
            builder.Property(q => q.OptionB).IsRequired().HasMaxLength(200);
            builder.Property(q => q.OptionC).IsRequired().HasMaxLength(200);
            builder.Property(q => q.OptionD).IsRequired().HasMaxLength(200);

            builder.Property(q => q.CorrectAnswer)
                .IsRequired()
                .HasMaxLength(1); 

            builder.HasOne(q => q.Quiz)
                .WithMany(qz => qz.Questions)
                .HasForeignKey(q => q.QuizId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
