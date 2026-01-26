using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Execptions;
using DomainLayer.Models.OrderModels;
using Microsoft.Extensions.Configuration;
using Services_Abstraction;
using Shared.DTOS.BasketDTO;
using Stripe;
using Product = Domain.Entitys.Product.Product;

namespace Services_Layer.Service.PaymentService
{
    public class PaymentService (IConfiguration _configuration,IBasketRepository basketRepository,IUnitOfWork unitOfWork) : IPaymentService
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
        }

    }
}
