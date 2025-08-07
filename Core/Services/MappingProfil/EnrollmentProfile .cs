using AutoMapper;
using Domain.Models;
using Share.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EnrollmentProfil
{
    public class EnrollmentProfile :Profile
    {
        public EnrollmentProfile()
        {
            CreateMap<Enrollment, EnrollmentDto>().ReverseMap();
        }
    }
}
