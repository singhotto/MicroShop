
using AutoMapper;
using Warehouse.Business.Factory;
using Warehouse.Repository;
using Warehouse.Repository.Model;
using Warehouse.Shared.Dto;

namespace Warehouse.Business;

public class Business : IBusiness
{

    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    public Business(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public void DeleteCategory(string id)
    {
        _repository.DeleteCategory(id);
    }

    public void DeleteProduct(string id)
    {
        _repository.DeleteProduct(id);
    }

    public List<CategoryDto> GetCategories()
    {
        return GetListCategoryDto(_repository.ReadCategory());
    }

    public CategoryDto? GetCategory(string id)
    {
        Category? category = _repository.GetCategoryById(id);

        if (category == null) return null;

        return _mapper.Map<CategoryDto>(category);
    }

    public ProductDto? GetProduct(string id)
    {
        Product? product = _repository.GetProductById(id);

        if (product == null) return null;

        return _mapper.Map<ProductDto>(product);
    }

    public List<ProductDto> GetProducts()
    {
        return GetListProductDto(_repository.ReadProduct());
    }


    private List<ProductDto> GetListProductDto(IEnumerable<Product> prodcuts)
    {
        return prodcuts.Select(_mapper.Map<ProductDto>).ToList();
    }
    private List<CategoryDto> GetListCategoryDto(IEnumerable<Category> category)
    {
        return category.Select(_mapper.Map<CategoryDto>).ToList();
    }

    public ProductInsertDto AddProduct(ProductInsertDto product)
    {
        Product model = _mapper.Map<Product>(product);

        _repository.CreateTransaction(() =>
        {
            _repository.Insert(model);
            _repository.SaveChanges();

            _repository.InsertTransactionalOutbox(TransactionalOutboxFactory.CreateInsert(_mapper.Map<ProductDto>(model)));
            _repository.SaveChanges();
        });

        return product;
    }

    public CategoryInsertDto AddCategory(CategoryInsertDto category)
    {
        Category model = _mapper.Map<Category>(category);
        _repository.Insert(model);
        _repository.SaveChanges();

        return category;
    }
}