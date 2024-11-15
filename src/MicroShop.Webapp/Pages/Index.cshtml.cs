using MicroShop.Business;
using MicroShop.Shared.Dto;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MicroShop.Webapp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private  IBusiness _business;
        public List<ProductDto>? _products = new List<ProductDto>();
        public IndexModel(IBusiness business, ILogger<IndexModel> logger)
        {
            _business = business;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            _products = await _business.GetProducts();
        }
    }
}
