using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entitys.Product;
using Domain.Execptions;
using Services_Abstraction;
using Services_Layer.Specifications;
using Shared;
using Shared.DTOS.Product;

namespace Services_Layer.Service.ProductService
{
    internal class ProductService(IUnitOfWork _uniteofwork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            var repo = _uniteofwork.GetRepository<ProductBrand, int>();
            var brand = await repo.GetAllAsync();
            var barndDTo = _mapper.Map<IEnumerable<BrandDTO>>(brand);
            return barndDTo;
        }

        public async Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var spec = new ProductWithTypeAndBrandSpecification(queryParams);
            var repo = _uniteofwork.GetRepository<Product, int>();
            var product = await repo.GetAllAsync(spec);
            var productDTO = _mapper.Map<IEnumerable<ProductDTO>>(product);
            var CountOfReturnedData = productDTO.Count();
            var CountSpec = new ProductCountSpecification(queryParams);
            var CountOfAllProducts = await _uniteofwork.GetRepository<Product, int>().CountAsync(CountSpec);
            return new PaginatedResult<ProductDTO>(queryParams.PageIndex, CountOfReturnedData, CountOfAllProducts, productDTO);
        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var repo = _uniteofwork.GetRepository<ProductType, int>();
            var type = await repo.GetAllAsync();
            var typeDTO = _mapper.Map<IEnumerable<TypeDTO>>(type);
            return typeDTO;
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithTypeAndBrandSpecification(id);
            var Product = await _uniteofwork.GetRepository<Product, int>().GetByIdAsync(spec);
            if (Product is null) throw new ProductNotFoundException(id);
            var productDTO = _mapper.Map<ProductDTO>(Product);
            return productDTO;

        }
    }
}
