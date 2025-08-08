using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IServiceManager
    {
        public ICourseServices CourseServices { get; }
        public ICloudinaryService CloudinaryService { get; }
        public IMailServices MailServices { get; }
        public IModuleServices ModuleServices { get; }
        public ILessonServices LessonServices { get; }
        public IQuizServices QuizServices { get; }
        public ICategoryServices CategoryServices { get; }
        public IProgressServices ProgressServices { get; }
        public IEnrollmentServices EnrollmentServices { get; }
        public IReviewServices ReviewServices { get; }
        public IAuthenticationServices AuthenticationServices { get; }
        public IBasketService BasketService { get; }
        public IOrederService OrederService { get; }

      
    }
}
