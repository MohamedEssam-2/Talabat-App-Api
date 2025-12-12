//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using TalabatDemo.Models;

//namespace TalabatDemo.Controllers
//{
//    #region Notes
//    //     Api    => (Application Programming Interface) its a set of rules that allows different applications applications to communicate with each other via (http Request)Url Such as Frontend(React app,Request ) With Backend(.Net app , Response) 

//    //Types of Apis?
//    //Restful api => Return Json File,xml,... and its fixed cant change the response. if you request employee data that mean its return all employee data as a Json file (Cannot specify the response)
//    //            => uses HTTP methods like GET, POST, PUT, DELETE

//    //   SOAP     => Return Xml File Contain the Data ,Used in Security and Monitoring the the performance of the response (For example can be Used in payment Getway,Transactions ).

//    //   GrapghQL => Return Query and  Can specify the response of the data 

//    //   RPC,GRPC (Remote procedure call ) => Used with Microservices Projects , Developed by google 

//    #endregion

//    #region Onion Architecture


//    #region Core
//    // 1. Core Layer => Contains Domain ,Services,Services Abstractions

//    //Domain =>Contains Entities , Exceptions , GLobal , Repo Contracts  ,   Dont See Any other Project
//    //Services Abstractions => Interfaces for Services(Contracts)        ,   Can See Shared Only
//    //Services => Implementation of Services Abstractions ,Mapping       ,   Can see Domain and Services Abstractions Only
//    #endregion

//    #region Infrastructure
//    //2. Infrastructure Layer =>Contains Presistence Layer, Presentation Layer
//    //  Presentation Layer (API,Controllers,Middlewares)                      , Can See Services Implementation and Shared Only
//    //  Presistence Layer (Database Context,EF Configurations,Repositories)   , Can See Domain and Services
//    #endregion


//    #region Web Application Layer 
//    //3. Web Application Layer => Can See All the Pervious Projects and Contains DI Container , Program , wwwroot,..... 
//    //Can See Presentation Layer , Presistence Layer
//    #endregion

//    #region Shared 
//    //4.Shared => Contains All Dtos , Dont See Any other Project
//    #endregion

//    #endregion



//    [Route("api/[controller]")]

//    [ApiController]//Check ModelState Validation
//    //If There any required attribute does not assign will return error number 400 before enter the Action
//    public class ProductController : ControllerBase
//    {
//        [HttpGet]
//        public ActionResult GetProducts()
//        {
//            var products = new[]
//            {
//                new { Id = 1, Name = "Product 1" },
//                new { Id = 2, Name = "Product 2" },
//                new { Id = 3, Name = "Product 3" }
//            };
//            return Ok(products);
//        }

//        [HttpGet("{id}")]//BaseUrl/api/Product/{id}
//        public ActionResult<Product> GetProductById(int id)
//        {
//            return new Product (){ Id = id};
//        }

//        [HttpPost]
//        public ActionResult<Product> AddProduct(Product product)
//        {
//            return new Product() { Id = 9, Name = "Mohamed" };
//        }
//        [HttpDelete]
//        public ActionResult<bool> DeleteProduct(int id)
//        {
//            return id>20 ? true : false;
//        }
//        [HttpPut]
//        public ActionResult<Product> UpdateProduct(Product product)
//        {
//            return new Product() { Id = 11, Name = "Mohsen" };
//        }

//    }
//}
