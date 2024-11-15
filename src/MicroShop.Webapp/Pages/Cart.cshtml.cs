using MicroShop.Business;
using MicroShop.Shared.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.ObjectModel;
using System.Net;

namespace MicroShop.Webapp.Pages
{
    public class CartModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;
        private IBusiness _business;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public List<CartDto>? _cartItems = new List<CartDto>();
        public string accessToken = string.Empty;
        public CartModel(IHttpContextAccessor httpContextAccessor, IBusiness business, ILogger<IndexModel> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _business = business;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            var cookies =  _httpContextAccessor.HttpContext.Request.Cookies;
            foreach (var cookie in cookies)
            {
                accessToken += "" + cookie.Key + "=" + cookie.Value+"; ";
            }
            Console.WriteLine(accessToken);
            if (accessToken != "")
            {
                _cartItems = await _business.GetCartItemsAuth(accessToken);
            }
        }
    }
}
