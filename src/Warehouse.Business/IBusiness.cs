using Warehouse.Shared.Dto;

namespace Warehouse.Business
{
    public interface IBusiness
    {
        public List<ProductDto> GetProducts();

        public ProductDto? GetProduct(string id);
        public ProductInsertDto AddProduct(ProductInsertDto product);

        public void DeleteProduct(string id);

        public List<CategoryDto> GetCategories();

        public CategoryDto? GetCategory(string id);

        public CategoryInsertDto AddCategory(CategoryInsertDto category);

        public void DeleteCategory(string id);
    }
}
