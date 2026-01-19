using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation_Layer.Attributes;
using Services_Abstraction;
using Shared;
using Shared.DTOS.Product;

namespace Presentation_Layer.Controllers
{
    [ApiController]
    [Route("api/[Controller]")] //BaseURL/api
    //[Authorize]// allow all to access Product controller 
    public class ProductController(IServiceManager serviceManger):ControllerBase
    {

        //Get All Products
        [CacheRedis]
        [HttpGet] //BaseURL/api/Product
        //[Authorize()]
        public async Task<ActionResult<PaginatedResult<ProductDTO>>> GetAllProductsAsync([FromQuery] ProductQueryParams queryParams)
        {
            var products = await serviceManger.ProductService.GetAllProductsAsync(queryParams);
            return Ok(products);
        }

        //Get Product By Id
        [HttpGet("{id}")] //BaseURL/api/Product/2
        public async Task<ActionResult<ProductDTO>> GetProductByIdAsync(int id)
        {
            var product = await serviceManger.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }

        //Get all Brands
        [HttpGet("brands")] //BaseURL/api/Product/brands/
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrandsAsync()
        {
            var brands = await serviceManger.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }

        //Get All Types
        [HttpGet("types")] //BaseURL/api/Product/types/
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllTypesAsync()
        {
            var types = await serviceManger.ProductService.GetAllTypesAsync();
            return Ok(types);
        }

    }
}
