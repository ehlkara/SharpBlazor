using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sharp_Business.Repository.IRepository;
using Sharp_Common;
using Sharp_Models;

namespace SharpWeb_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productRepository.GetAll());
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> Get(int? productId)
        {
            if(productId == null || productId == 0)
            {
                return BadRequest(new ErrorModelDto()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var product = await _productRepository.Get(productId.Value);
            if(product == null)
            {
                return BadRequest(new ErrorModelDto()
                {
                    ErrorMessage = "Product not found.",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(product);
        }
    }
}
