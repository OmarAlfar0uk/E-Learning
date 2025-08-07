using AutoMapper;
using CloudinaryDotNet;
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
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper , UserManager<AppUsers> _userManager , IConfiguration _configuration , IOptions<MailSettings> _options ,IMailServices _mailServices ,ICloudinaryService _cloudinaryService , Cloudinary _cloudinary) : IServiceManager
    {
        private readonly Lazy<ICourseServices> _LazeCourseServices = new Lazy<ICourseServices>(() => new CourseServices(_unitOfWork, _mapper, _cloudinary, _cloudinaryService));
        private readonly Lazy<IModuleServices> _LazeModuleServices = new Lazy<IModuleServices>(() => new ModuleServices(_unitOfWork, _mapper));
        private readonly Lazy<ILessonServices> _LazeLessonServices = new Lazy<ILessonServices>(() => new LessonServices(_unitOfWork, _mapper , _cloudinaryService));
        private readonly Lazy<IQuizServices> _LazeQuizServices = new Lazy<IQuizServices>(() => new QuizServices(_unitOfWork, _mapper ));
        private readonly Lazy<ICategoryServices> _LazeCategoryServices = new Lazy<ICategoryServices>(() => new CategoryServices(_unitOfWork, _mapper ));
        private readonly Lazy<IEnrollmentServices> _LazeEnrollmentServices = new Lazy<IEnrollmentServices>(() => new EnrollmentServices(_unitOfWork, _mapper));
        private readonly Lazy<IReviewServices> _LazeReviewServices = new Lazy<IReviewServices>(() => new ReviewServices(_unitOfWork, _mapper));
        private readonly Lazy<IProgressServices> _LazeProgressServices = new Lazy<IProgressServices>(() => new ProgressServices(_unitOfWork, _mapper));
        private readonly Lazy<IAuthenticationServices> _LazeAuthenticationServices = new Lazy<IAuthenticationServices>(() => new AuthenticationServices(_userManager , _configuration , _mailServices));
        private readonly Lazy<IMailServices> _LazeMailServices = new Lazy<IMailServices>(() => new MailServices(_options));
        private readonly Lazy<ICloudinaryService> _LazeCloudinaryService = new Lazy<ICloudinaryService>(() => new CloudinaryService(_configuration));
        public ICourseServices CourseServices => _LazeCourseServices.Value;
        public IModuleServices ModuleServices => _LazeModuleServices.Value;
        public ILessonServices LessonServices => _LazeLessonServices.Value;
        public IQuizServices QuizServices => _LazeQuizServices.Value;
        public IEnrollmentServices EnrollmentServices => _LazeEnrollmentServices.Value;
        public IReviewServices ReviewServices => _LazeReviewServices.Value;
        public IProgressServices ProgressServices => _LazeProgressServices.Value;
        public IAuthenticationServices AuthenticationServices => _LazeAuthenticationServices.Value;
        public ICategoryServices CategoryServices => _LazeCategoryServices.Value;
        public IMailServices MailServices => _LazeMailServices.Value;
        public ICloudinaryService CloudinaryService => _LazeCloudinaryService.Value;
    }
}
