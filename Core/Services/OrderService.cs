using AutoMapper;
using Domain.Contract;
using Domain.Excptions;
using Domain.Models;
using Domain.Models.OrderModule;
using Services.Specifications;
using Services.Specifications.OrderModuleSpecification;
using ServicesAbstraction;
using Share.DataTransferObject.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService(IMapper _mapper, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork) : IOrederService
    {

        private static OrderItem CreateOrderItem(Domain.Models.BasketModuel.BasketItem item, Course Course)
        {
            return new OrderItem()
            {
                Course = new CourseItemOrder() { CoursetId = Course.Id, PictureUrl = Course.PictureUrl, CourseName = Course.Name },
                Price = Course.Price
              
            };
        }




        public async Task<OrderToReturnDTo> CreateOrderAsync(OrderDTo orderDTo, string Email)
        {
            
            //Get Basket
            var Basket = await _basketRepository.GetBasketAsync(orderDTo.BasketId) ?? throw new BasketNotFoundExcptions(orderDTo.BasketId);

            ArgumentNullException.ThrowIfNullOrEmpty(Basket.PaymentIntentId);
            var OrderRepo = _unitOfWork.GetRepository<Order, Guid>();
            var OrderSpec = new OrderWhithPaymentIntentIdSpecifications(Basket.PaymentIntentId);
            var ExtistingOrder = await OrderRepo.GetByIdAsync(OrderSpec);
            if (ExtistingOrder is not null)
                OrderRepo.Remove(ExtistingOrder);

            //Create Order Item List
            List<OrderItem> orderItems = [];
            var ProductRepo = _unitOfWork.GetRepository<Course, int>();
            foreach (var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id)
                    ?? throw new CourseNotFoundException(item.Id);

                orderItems.Add(CreateOrderItem(item, Product));
            }
            //Get Delivery method
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDTo.DeliveryMethodId)
                  ?? throw new DeliveryMethodNotFoundException(orderDTo.DeliveryMethodId);
            //Calcolate sub Total
          

            var Order = new Order(Email,  DeliveryMethod, orderItems, Basket.PaymentIntentId);


            await OrderRepo.AddAysnc(Order);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<Order, OrderToReturnDTo>(Order);
        }


        public async Task<IEnumerable<DeliveryMethodDTo>> GetDeliveryMethodAsync()
        {
            var DeliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDTo>>(DeliveryMethods);

        }

        public async Task<IEnumerable<OrderToReturnDTo>> GetAllOrdersAsync(string Email)
        {
            var Spec = new OrderSpecifications(Email);
            var Orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(Spec);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDTo>>(Orders);


        }

        public async Task<OrderToReturnDTo> GetOrderByIdAsync(Guid Id)
        {

            var Spec = new OrderSpecifications(Id);
            var Order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(Spec);
            return _mapper.Map<Order, OrderToReturnDTo>(Order);

        }
    }
}
