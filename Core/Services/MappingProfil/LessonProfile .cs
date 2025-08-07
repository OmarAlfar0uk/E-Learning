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
    internal class LessonProfile : Profile
    {
        public LessonProfile()
        {
       
            CreateMap<Lesson, LessonDto>()
                .ForMember(dest => dest.VideoFile, opt => opt.Ignore());

            CreateMap<LessonDto, Lesson>()
                .ForMember(dest => dest.VideoUrl, opt => opt.Ignore()); 
        }
    }
}
