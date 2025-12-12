using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Shared.DTOS.Product;

namespace Services_Abstraction
{
    public interface IProductService
    {
        //GetAll Product
        Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams queryParams);

        //Get Product By Id
        Task<ProductDTO> GetProductByIdAsync(int id);

        //GetAll Types
        Task<IEnumerable<TypeDTO>> GetAllTypesAsync();

        //GetAll Brands
        Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();

    }
}
