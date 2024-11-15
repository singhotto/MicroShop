using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Order.Repository;
using Order.Repository.Model;
using Order.Shared.Dto;

namespace Order.Business;

public class Business : IBusiness
{

    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    public Business(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public OrderDto AddOrder(OrderInsertDto order)
    {
        Orders newOrder = _mapper.Map<Orders>(order);

        if (order.ProductOrders != null && order.ProductOrders.Any())
        {
            List<OrderProductList> productOrders = _mapper.Map<List<OrderProductList>>(order.ProductOrders);

            newOrder.Products = productOrders;
        }

        _repository.Insert(newOrder);

        _repository.SaveChanges();

        return _mapper.Map<OrderDto>(newOrder);
    }

    public void AddProducts(List<ProductInsertDto> products)
    {
        foreach(ProductInsertDto product in products)
        {
            _repository.Insert(_mapper.Map<Product>(product));
        }

        _repository.SaveChanges();
    }
    public void DeleteOrder(int id)
    {
        _repository.DeleteOrder(id);
    }


    public OrderDto? GetOrder(int id)
    {
        Orders? o = _repository.GetOrderById(id);

        if (o == null) return null;

        return _mapper.Map<OrderDto>(o);
    }

    public List<OrderInfoDto> GetOrderOfUser(string userId, string status = "", DateTime from = default, DateTime to = default)
    {
        if (from == default)
            from = new DateTime(2022, 01, 01);

        if (to == default)
            to = DateTime.Now;

        IQueryable<Orders> query = _repository.ReadOrder()
            .Include(o => o.Products)
                .ThenInclude(p => p.Product)
            .Where(o => o.User_Id == userId && o.Created_At >= from && o.Created_At <= to);

        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(o => o.Order_Status == status);
        }
        List<OrderInfoDto> result = query
            .Select(o => new OrderInfoDto
            {
                Order_Id = o.Order_Id,
                User_Name = $"{o.User.FirstName} {o.User.LastName}",
                User_Address = o.User.Address,
                Tracking_Number = o.Tracking_Number,
                Order_Status = o.Order_Status,
                Amount = o.Amount,
                Created_At = o.Created_At,
                Products = o.Products.Select(p => _mapper.Map<ProductDto>(p.Product)).ToList(),
            })
            .ToList();

        return result;
    }

    public List<OrderDto> GetOrders()
    {
       return _repository.ReadOrder().AsEnumerable().Select(_mapper.Map<OrderDto>).ToList();
    }

    public ProductDto? GetProduct(string id)
    {
        return _mapper.Map<ProductDto>(_repository.GetProductById(id));
    }


    public List<ProductDto> GetProducts()
    {
        return _repository.ReadProduct().AsEnumerable().Select(_mapper.Map<ProductDto>).ToList();
    }

    public OrderDto? UpdateOrder(int id, OrderDto order)
    {
        return _mapper.Map<OrderDto>(_repository.UpdateOrder(id, _mapper.Map<Orders>(order)));
    }

    public OrderDto? UpdateOrderStatus(int id, string status)
    {
        return _mapper.Map<OrderDto>(_repository.UpdateOrderStatus(id, status));
    }

}