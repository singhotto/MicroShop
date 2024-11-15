using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Supply.Business;
using Supply.Shared.Dto;

namespace Supply.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SupplyController : ControllerBase
    {

        private readonly IBusiness _business;
        private readonly ILogger<SupplyController> _logger;

        public SupplyController(IBusiness business, ILogger<SupplyController> logger)
        {
            _business = business;
            _logger = logger;
        }

        #region Supplier

        [HttpGet(Name = "GetSuppliers")]
        [Authorize(Roles = "Admin")]
        public ActionResult<SupplierDto> GetSuppliers() { 
            return Ok(_business.GetSuppliers());
        }

        [HttpGet(Name = "GetSupplier")]
        [Authorize(Roles = "Admin")]
        public ActionResult<SupplierDto> GetSupplier(string id)
        {
            return Ok(_business.GetSupplier(id));
        }

        [HttpPost(Name = "AddSupplier")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddSupplier(string supplierEmail)
        {
            var cookies = Request.Cookies;
            string accessToken = "";
            foreach (var cookie in cookies)
            {
                accessToken += "" + cookie.Key + "=" + cookie.Value + "; ";
            }

            if (string.IsNullOrEmpty(accessToken))
                return Unauthorized();

            SupplierDto result = await _business.AddSupplier(supplierEmail, accessToken);

            if (result != null)
                return Ok(result);
            else
                return NotFound(); 
        }

        [HttpPost(Name = "AddProductsAsSupplier")]
        [Authorize(Roles = "Supplier")]
        public ActionResult AddProductsAsSupplier(List<ProductInsertAutorized> products)
        {
            List<ProductInsertDto> prorductsAuthed = new List<ProductInsertDto>();
            foreach (var product in products)
            {
                prorductsAuthed.Add(new ProductInsertDto()
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Category_Id = product.Category,
                    Supplier_Id = User.Identity.Name
                });
            }
            _business.AddProducts(prorductsAuthed);
            return Ok("Products added successfuly");
        }

        [HttpPost(Name = "AddRandomProductsAsSupplier")]
        [Authorize(Roles = "Supplier")]
        public ActionResult AddRandomProductsAsSupplier()
        {
            _business.AddRandomProducts(User.Identity.Name);
            return Ok("Random Products added successfuly");
        }

        [HttpPut(Name = "UpdateSupplier")]
        [Authorize(Roles = "Admin")]
        public ActionResult<SupplierDto> UpdateSupplier(string id, SupplierDto supplier)
        {
            return Ok(_business.UpdateSupplier(id, supplier));
        }

        [HttpDelete(Name = "DeleteSupplier")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteSupplier(string id)
        {
            _business.DeleteSupplier(id);
            return Ok("Supplier with id "+id+" deleted!");
        }

        #endregion Supplier

        #region Order

        [HttpPost(Name = "NewOrder")]
        [Authorize(Roles = "Admin")]
        public ActionResult<OrderDto> NewOrder(string supplierId, List<OrderProductInsertDto> productList)
        {
            return Ok(_business.AddOrder(supplierId, productList));
        }

        [HttpGet(Name = "GetOrdersOfSupplier")]
        [Authorize(Roles = "Admin, Supplier")]
        public ActionResult<List<OrderDto>> GetOrdersOfSupplier(string supplierId = "", string status = "", DateTime from = default, DateTime to = default)
        {
            if (User.IsInRole("Supplier")) supplierId = User.Identity.Name;
            if (supplierId == "") return Unauthorized();
            return Ok(_business.GetOrderOfSupplier(supplierId, status, from, to));
        }

        [HttpPut(Name = "UpdateOrderStatus")]
        [Authorize(Roles = "Supplier")]
        public ActionResult<OrderDto> UpdateOrderStatus(int order_id,  string status)
        {
            return Ok(_business.UpdateOrderStatus(order_id, status));
        }
        #endregion Order
    }
}
