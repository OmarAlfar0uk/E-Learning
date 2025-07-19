using AutoMapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Share.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfil
{
    internal class PromoVideoUrlResolver(IConfiguration _configuration) : IValueResolver<Course, CourseDto, string>
    {
        public string Resolve(Course source, CourseDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PromoVideoUrl))
                return string.Empty;
            else
            {
                var Url = $"{_configuration.GetSection("Urls")["BaseUrl"]}";
                return Url;
            }
        }
    }
}
