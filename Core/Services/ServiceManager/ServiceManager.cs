using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entitys.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services_Abstraction;
using Services_Layer.Service;
using Services_Layer.Service.BasketService;
using Services_Layer.Service.OrderService;
using Services_Layer.Service.ProductService;
using Shared.Authenticaion;

namespace Services_Layer.ServiceManger
{
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper,IBasketRepository _basketRepository,UserManager<ApplicationUser>userManager,IConfiguration _configuration,IOptions<JwtOptions>_jwtOptions) : IServiceManager
    {
        //Contains All Services in one Place such as unitOfWork contains all Repositories
        //Lazy Loading for better Performance thats mean create service only when its needed 

        private readonly Lazy<IProductService> _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        public IProductService ProductService => _productService.Value;

        private readonly Lazy<IBasketService> _basketservice = new Lazy<IBasketService>(() => new BasketService(_basketRepository, mapper));
        public IBasketService BasketService => _basketservice.Value;

        private readonly Lazy<IAuthenticationService> _LazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, _configuration, _jwtOptions, mapper));
        public IAuthenticationService authenticationService => _LazyAuthenticationService.Value;

        private readonly Lazy<IOrderService> _lazyorderService = new Lazy<IOrderService>(() => new OrderService(mapper, _basketRepository,unitOfWork));
        public IOrderService OrderService => _lazyorderService.Value;


    }
}
