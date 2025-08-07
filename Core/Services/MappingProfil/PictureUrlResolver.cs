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
    public class PictureUrlResolver : IValueResolver<Course, CourseDto, string>
    {
        private readonly IConfiguration _configuration;

        public PictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Course source, CourseDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;

            var baseUrl = _configuration.GetSection("Urls")["CloudinaryBaseUrl"];
            return $"{baseUrl}/{source.PictureUrl}"; 
        }
    }
}
