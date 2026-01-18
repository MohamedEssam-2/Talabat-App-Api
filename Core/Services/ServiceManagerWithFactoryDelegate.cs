using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services_Abstraction;

namespace Services_Layer
{
    internal class ServiceManagerWithFactoryDelegate(Func<IProductService> productServiceFactory,
                                                     Func<IBasketService> basketServiceFactory,
                                                     Func<IOrderService> orderServiceFactory,
                                                     Func<IAuthenticationService> authenticationServiceFactory,
                                                     Func<ICacheService> cacheFactory) : IServiceManager
    {
        public IProductService ProductService => productServiceFactory.Invoke();

        public IBasketService BasketService => basketServiceFactory.Invoke();

        public IAuthenticationService authenticationService => authenticationServiceFactory.Invoke();

        public IOrderService OrderService => orderServiceFactory.Invoke();

        public ICacheService CacheService => cacheFactory.Invoke();
    }
}
