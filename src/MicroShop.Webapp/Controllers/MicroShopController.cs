using MicroShop.Business;
using MicroShop.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroShop.Webapp.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class MicroShopController : ControllerBase
    {
        private readonly IBusiness _business;
        private readonly ILogger<MicroShopController> _logger;

        public MicroShopController(IBusiness business, ILogger<MicroShopController> logger)
        {
            _business = business;
            _logger = logger;
        }

        [HttpPost(Name = "AddToCart")]
        [Authorize]
        public ActionResult<CartDto> AddToCart(CartInsertDto item)
        {
            item.UserName = User.Identity.Name;
            return Ok(_business.AddToCart(item));
        }

        [HttpGet(Name = "GetCartItems")]
        [Authorize]
        public ActionResult<List<CartDto>> GetCartItems()
        {
            string userEmail = User.Identity.Name;
            return Ok(_business.GetCartItems(userEmail));
        }

        [HttpPut("{ItemId}", Name = "IncreaseCartItem")]
        [Authorize]
        public  ActionResult<int> IncreaseCartItem(int ItemId)
        {
            string userEmail = User.Identity.Name;
            if (userEmail == null)
                return Unauthorized();
            return Ok(_business.IncreaseCartItems(userEmail, ItemId));
        }

        [HttpPut("{ItemId}", Name = "DecreaseCartItem")]
        [Authorize]
        public ActionResult<int> DecreaseCartItem(int ItemId)
        {
            string userEmail = User.Identity.Name;
            if (userEmail == null)
                return Unauthorized();
            return Ok(_business.DecreaseCartItems(userEmail, ItemId));
        }

        [HttpDelete(Name = "EmptyCart")]
        [Authorize]
        public ActionResult EmptyCart()
        {
            string userEmail = User.Identity.Name;
            if(userEmail == null)
                return Unauthorized();
            _business.emptyCart(userEmail);
            return Ok("{ Empty: true }");
        }

        [HttpPost(Name = "MakeSupplier")]
        [Authorize(Roles = "Admin")]
        public async Task<UserDto> MakeSupplierAsync(SupplierDto data)
        {
            return await _business.MakeSupplierAsync(data.supplierEmail);
        }
    }
}
