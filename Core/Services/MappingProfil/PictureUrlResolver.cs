using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.Internal;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Share.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfil
{
    internal class PictureUrlResolver(IConfiguration _configuration) : IValueResolver<Course, CourseDto, string>
    {
        public string Resolve(Course source, CourseDto destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;
            else
            {
                var Url = $"{_configuration.GetSection("Urls")["BaseUrl"]}";
                    return Url;
            }
        }
    }
}
