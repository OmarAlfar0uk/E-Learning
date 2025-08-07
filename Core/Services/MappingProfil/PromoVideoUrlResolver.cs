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
    public class PromoVideoUrlResolver : IValueResolver<Course, CourseDto, string>
    {
        private readonly IConfiguration _configuration;

        public PromoVideoUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Course source, CourseDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PromoVideoUrl))
                return string.Empty;

            var baseUrl = _configuration.GetSection("Urls")["CloudinaryBaseUrl"];
            return $"{baseUrl}/{source.PromoVideoUrl}"; 
        }
    }
}
