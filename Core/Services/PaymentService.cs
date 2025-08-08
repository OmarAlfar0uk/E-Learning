using AutoMapper;
using Domain.Contract;
using Domain.Excptions;
using Domain.Models;
using Domain.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using ServicesAbstraction;
using Share.DataTransferObject.BasketModuleDtos;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PaymentService(IConfiguration _configration, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork, IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string BasketId)
        {
            StripeConfiguration.ApiKey = _configration["StripeSetting:SecretKey"];

            var Basket = await _basketRepository.GetBasketAsync(BasketId) ?? throw new BasketNotFoundExcptions(BasketId);

            var ProudectRepo = _unitOfWork.GetRepository<Course, int>();
            foreach (var item in Basket.Items)
            {
                var Product = await ProudectRepo.GetByIdAsync(item.Id) ?? throw new CourseNotFoundException(item.Id);
                item.Price = Product.Price;
            }
            ArgumentNullException.ThrowIfNull(Basket.deliveryMethodId);
            var DelivreMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(Basket.deliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(Basket.deliveryMethodId.Value);
            Basket.shippingPrice = DelivreMethod.Price;



            var PaymentService = new PaymentIntentService();
            if (Basket.PaymentIntentId is null) //Create
            {
                var Option = new PaymentIntentCreateOptions()
                {

                    Currency = "USD",
                    PaymentMethodTypes = ["card"]
                };
                var PaymentIntent = await PaymentService.CreateAsync(Option);
                Basket.PaymentIntentId = PaymentIntent.Id;
                Basket.clintSecret = PaymentIntent.ClientSecret;
            }
            else //Update
            {
                var Option = new PaymentIntentUpdateOptions();
                await PaymentService.UpdateAsync(Basket.PaymentIntentId, Option);

            }

            await _basketRepository.CreateOrUpdatBaskAsync(Basket);

            return _mapper.Map<BasketDto>(Basket);
        }

    }
}