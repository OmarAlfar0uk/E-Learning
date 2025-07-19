using AutoMapper;
using Domain.Contract;
using Domain.Models.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper , UserManager<AppUsers> _userManager , IConfiguration _configuration) : IServiceManager
    {
        private readonly Lazy<ICourseServices> _LazeCourseServices = new Lazy<ICourseServices>(() => new CourseServices(_unitOfWork ,_mapper));
        private readonly Lazy<IAuthenticationServices> _LazeAuthenticationServices = new Lazy<IAuthenticationServices>(() => new AuthenticationServices(_userManager , _configuration));
        public ICourseServices CourseServices => _LazeCourseServices.Value;

        public IAuthenticationServices AuthenticationServices => _LazeAuthenticationServices.Value;
    }
}
