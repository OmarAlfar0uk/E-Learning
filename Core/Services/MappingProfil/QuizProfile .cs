using AutoMapper;
using Domain.Models;
using Share.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfil
{
    internal class QuizProfile : Profile
    {
        public QuizProfile()
        {
            CreateMap<Quiz, QuizDto>()
               .ReverseMap();

            CreateMap<QuizQuestion, QuizQuestionDto>()
                .ReverseMap();

            CreateMap<QuizEvaluationResultDto, QuizEvaluationResultDto>(); 
            CreateMap<QuizStatisticsDto, QuizStatisticsDto>();
        }
    }
}
