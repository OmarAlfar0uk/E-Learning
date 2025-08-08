using AutoMapper;
using Domain.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using Share.DataTransferObject.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfil
{
    internal class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDTo, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDTo destination, string destMember, ResolutionContext context)
        {

            if (string.IsNullOrEmpty(source.Course.PictureUrl))
                return string.Empty;
            else
            {
                /*                var Url = $"https://localhost:7061/{source.PictureUrl}";
                */
                var Url = $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.Course.PictureUrl}";
                return Url;
            }

        }
    }
}
