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
    public class ProgressProfile : Profile
    {
        public ProgressProfile()
        {
            CreateMap<Progress, ProgressDto>().ReverseMap();

        }
    }
}
