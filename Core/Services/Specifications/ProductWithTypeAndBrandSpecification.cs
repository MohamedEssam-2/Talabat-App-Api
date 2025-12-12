using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entitys.Product;
using Shared;

namespace Services_Layer.Specifications
{
    internal class ProductWithTypeAndBrandSpecification : BaseSpecification<Product, int>
    {
        //Get Product By Id
        public ProductWithTypeAndBrandSpecification(int id):base(p=>p.Id==id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }


        //Get All Products 
        public ProductWithTypeAndBrandSpecification(ProductQueryParams queryParams) : base(p => (queryParams.BrandId == null || p.BrandId == queryParams.BrandId)
                          && (queryParams.TypeId == null || p.TypeId == queryParams.TypeId)
                          && ((string.IsNullOrEmpty(queryParams.Search))||p.Name.ToLower().Contains(queryParams.Search.ToLower())))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

            switch (queryParams.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderByAscending(p => p.Name);
                    break;

                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;

                case ProductSortingOptions.PriceAsc:
                    AddOrderByAscending(p => p.Price);
                    break;

                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;

                default:
                    AddOrderByAscending(p => p.Id);
                    break;

            }

            ApplyPagination(queryParams.PageSize,queryParams.PageIndex);
        }
    }
}
