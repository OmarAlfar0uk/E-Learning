using AutoMapper;
using Domain.Models;
using Microsoft.Extensions.Options;
using Share.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfil
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseDto>()
               .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.CourseType.Name))
               .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
               .ForMember(dist => dist.PictureUrl, opt => opt.MapFrom<PictureUrlResolver>())
               .ForMember(dist => dist.PromoVideoUrl, opt => opt.MapFrom<PromoVideoUrlResolver>());
        }
    }
}
