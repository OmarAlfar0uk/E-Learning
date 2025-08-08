using AutoMapper;
using Domain.Models.OrderModule;
using Share.DataTransferObject.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfil
{
    internal class OrderProfile : Profile
    {
        public OrderProfile()
        {

            CreateMap<Order, OrderToReturnDTo>()
                .ForMember(D => D.DeliveryMethod, O => O.MapFrom(S => S.DeliveryMethod.ShortName));
            CreateMap<OrderItem, OrderItemDTo>()
                .ForMember(D => D.CourseName, O => O.MapFrom(S => S.Course.CourseName))
                .ForMember(D => D.PictureUrl, O => O.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<DeliveryMethod, DeliveryMethodDTo>();
        }
    }
}
