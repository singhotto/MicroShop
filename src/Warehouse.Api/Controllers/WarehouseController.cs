using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Warehouse.Business;
using Warehouse.Shared.Dto; // Add this namespace for ILogger

namespace Warehouse.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WarehouseController : ControllerBase
    {

        private readonly IBusiness _business;
        private readonly ILogger<WarehouseController> _logger;

        public WarehouseController(IBusiness business, ILogger<WarehouseController> logger)
        {
            _business = business;
            _logger = logger;
        }

        [HttpGet(Name = "GetAllProducts")]
        public ActionResult<List<ProductDto>> GetAllProducts()
        {
            return Ok(_business.GetProducts());
        }

        [HttpGet(Name = "GetProductWithId")]
        public ActionResult<ProductDto> GetProductWithId(string id)
        {
            return Ok(_business.GetProduct(id));
        }

        [HttpPost(Name = "InsertProduct")]
        [Authorize(Roles = "Admin")]
        public ActionResult<ProductInsertDto> InsertProduct(ProductInsertDto product)
        {
            return Ok(_business.AddProduct(product));
        }

        [HttpGet(Name = "GetAllCategories")]
        public ActionResult<List<CategoryDto>> GetAllCategories()
        {
            return Ok(_business.GetCategories());
        }

        [HttpPost(Name = "InsertCategory")]
        [Authorize(Roles = "Admin")]
        public ActionResult<CategoryInsertDto> InsertCategory(CategoryInsertDto category)
        {
            return Ok(_business.AddCategory(category));
        }
    }
}
