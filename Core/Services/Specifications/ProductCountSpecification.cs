using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entitys.Product;
using Shared;

namespace Services_Layer.Specifications
{
    internal class ProductCountSpecification : BaseSpecification<Product, int>
    {
        public ProductCountSpecification(ProductQueryParams queryParams) : base(p => (queryParams.BrandId == null || p.BrandId == queryParams.BrandId)
                          && (queryParams.TypeId == null || p.TypeId == queryParams.TypeId)
                          && ((string.IsNullOrEmpty(queryParams.Search)) || p.Name.ToLower().Contains(queryParams.Search.ToLower())))
        {
        }
    }
}
