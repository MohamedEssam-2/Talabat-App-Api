using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Execptions;
using DomainLayer.Models.OrderModels;
using Microsoft.Extensions.Configuration;
using Services_Abstraction;
using Services_Layer.Specifications;
using Shared.DTOS.BasketDTO;
using Stripe;
using Product = Domain.Entitys.Product.Product;

namespace Services_Layer.Service.PaymentService
{
    public class PaymentService (IConfiguration _configuration,IBasketRepository basketRepository,IUnitOfWork unitOfWork , IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDTO> CreateOrUpdatePaymentIntent(string basketId)
        {
            //Get Secret Key from Configuration
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];
            //GEt BaskedId
            var basket = await basketRepository.GetBasketAsync(basketId)??throw new BasketNotFoundExceptions(basketId);
            //Calculate Total Amount
            //total=Product Price * Quantity+ DeliveryPrice
            //Get Product Price From Basket Items
            var ProductRepo=unitOfWork.GetRepository<Product,int>();
            foreach (var ProductInBasket in basket.Items)
            {
                var OriginalProduct= await ProductRepo.GetByIdAsync(ProductInBasket.Id)
                    ??throw new ProductNotFoundException(ProductInBasket.Id);
                ProductInBasket.Price = OriginalProduct.Price;
            }
            //Get Delivery Method
            var DeliveryMethod=unitOfWork.GetRepository<DeliveryMethod,int>();
            ArgumentNullException.ThrowIfNull(basket.DeliveryMethodId);
            var Delivery = await DeliveryMethod.GetByIdAsync(basket.DeliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);

            //Calculate Total Amount =Product Price * Quantity+ DeliveryPrice
            basket.ShippingPrice = Delivery.Price;
            var totalAmount = (long)(basket.Items.Sum(i => i.Price * i.Quantity )+Delivery.Price) * 100 ;

            //Create Payemnt Intent Service
            var service = new PaymentIntentService();
            if (basket.PaymentIntentId is null)
            {
                var options= new PaymentIntentCreateOptions
                {
                    Amount = totalAmount,
                    Currency = "USD",
                    PaymentMethodTypes = [ "card" ]
                };
                var intent = await service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = totalAmount
                };
                   await service.UpdateAsync(basket.PaymentIntentId, options);

            }
            await basketRepository.CreateOrUpdate(basket);
            //Map Basket to BasketDTO
            return _mapper.Map<BasketDTO>(basket);
        }

        public async Task UpdatePaymentStatus(string jsonRequest, string stripeHeader)
        {
            var stripeEvent = EventUtility.ConstructEvent(jsonRequest,
                      stripeHeader, _configuration["StripeSettings:EndPointSecret"]);


            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
            {
                await UpdatePaymentFailedAsync(paymentIntent.Id);
            }
            else if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                await UpdatePaymentReceivedAsync(paymentIntent.Id);

            }
            // ... handle other event types
            else
            {
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }
        }

        private async Task UpdatePaymentReceivedAsync(string paymentIntentId)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>()
                .GetByIdAsync(new OrderWithPaymentIntentIdSpecification(paymentIntentId));

            order.OrderStatus = OrderStatus.PaymentReceived;
            unitOfWork.GetRepository<Order, Guid>().Update(order);
            await unitOfWork.SaveChangesAsync();
        }

        private async Task UpdatePaymentFailedAsync(string paymentIntentId)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>()
                .GetByIdAsync(new OrderWithPaymentIntentIdSpecification(paymentIntentId));

            order.OrderStatus = OrderStatus.PaymentFailed;
            unitOfWork.GetRepository<Order, Guid>().Update(order);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
