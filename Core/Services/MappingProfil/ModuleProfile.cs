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
    public class ModuleProfile :Profile
    {
        public ModuleProfile()
        {
            CreateMap<Module, ModuleDto>().ReverseMap();

        }
    }
}
