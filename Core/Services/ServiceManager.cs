using AutoMapper;
using Domain.Contract;
using Domain.Models.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ServicesAbstraction;
using Share.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper , UserManager<AppUsers> _userManager , IConfiguration _configuration , IOptions<MailSettings> _options ,IMailServices _mailServices) : IServiceManager
    {
        private readonly Lazy<ICourseServices> _LazeCourseServices = new Lazy<ICourseServices>(() => new CourseServices(_unitOfWork ,_mapper));
        private readonly Lazy<IAuthenticationServices> _LazeAuthenticationServices = new Lazy<IAuthenticationServices>(() => new AuthenticationServices(_userManager , _configuration , _mailServices));
        private readonly Lazy<IMailServices> _LazeMailServices = new Lazy<IMailServices>(() => new MailServices(_options));
        public ICourseServices CourseServices => _LazeCourseServices.Value;
        public IAuthenticationServices AuthenticationServices => _LazeAuthenticationServices.Value;
        public IMailServices MailServices => _LazeMailServices.Value;
    }
}
