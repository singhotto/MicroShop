using AutoMapper;
using MicroShop.Client.Http.Abstractions;
using MicroShop.Repository;
using MicroShop.Repository.Model;
using MicroShop.Shared.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MicroShop.Business;

public class Business : IBusiness
{
    private readonly IMicroShopClient _microShopClient;
    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    public Business(IRepository repository, IMapper mapper, IMicroShopClient microShopClient)
    {

        _repository = repository;
        _mapper = mapper;
        _microShopClient = microShopClient;
    }
    public async Task<List<ProductDto>?> GetProducts(CancellationToken cancellationToken = default)
    {
        return await _microShopClient.GetProducts(cancellationToken);
    }

    public CartDto AddToCart(CartInsertDto item)
    {
        ApplicationUser? user = GetUser(item.UserName);
        if (user == null)
        {
            throw new Exception("Wrong Email");
        }
        for(int i = 0; i<10; i++)
            Console.WriteLine("Desi Crew");
        CartItem? cartItem = _repository.ReadCart().FirstOrDefault(c => (c.Product_Id == item.Product_Id && c.UserId == user.Id));

        if (cartItem == null)
        {
            cartItem = _mapper.Map<CartItem>(item);
            cartItem.UserId = user.Id;
            cartItem.Stock_Quantity = 1;

            _repository.Insert(cartItem);
            _repository.SaveChanges();
        }

        return _mapper.Map<CartDto>(cartItem);
    }

    public List<CartDto> GetCartItems(string userEmail)
    {
        ApplicationUser? user = GetUser(userEmail);
        if (user == null)
            throw new Exception("User not found");

        List<CartItem> items = _repository.ReadCart().Where(c => c.UserId == user.Id).ToList();

        return _mapper.Map<List<CartDto>>(items);
    }

    private ApplicationUser? GetUser(string userEmail)
    {
        return _repository.ReadUser().Where(x => x.UserName == userEmail).FirstOrDefault();
    }

    public async Task<List<CartDto>?> GetCartItemsAuth(string accessToken, CancellationToken cancellationToken = default)
    {
        
        return await _microShopClient.GetCartItems(accessToken, cancellationToken);
    }

    public void emptyCart(string userEmail)
    {
        ApplicationUser? user = GetUser(userEmail);
        List<CartItem> items = _repository.ReadCart().Where(c => c.UserId == user.Id).ToList();

        if (user == null)
            throw new Exception("User not found");

        if (items == null || items.Count == 0) return;

        _repository.DeleteRange(items);
        _repository.SaveChanges();
    }

    public async Task<UserDto> MakeSupplierAsync(string userEmail)
    {
        ApplicationUser? user = GetUser(userEmail);
        if (user == null)
            throw new Exception("User not found");

        IdentityResult res =  await _repository.MakeSupplierAsync(user);
        _repository.SaveChanges();

        if (res.Succeeded)
            return new UserDto()
            {
                User_Id = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address
            };

        return null;

    }

    private int modifyCartItemQuantity(string userEmail, int itemId, char operation)
    {
        ApplicationUser? user = GetUser(userEmail);
        if (user == null)
            throw new Exception("User not found");

        CartItem? item = _repository.ReadCart().FirstOrDefault(x => (x.Id == itemId && x.UserId == user.Id));

        if (item == null)
            throw new Exception("Cart Item not found");

        if (operation == '+')
            item.Stock_Quantity++;
        if (operation == '-')
        {
            item.Stock_Quantity--; 
            if(item.Stock_Quantity == 0)
            {
                _repository.Delete(item);
            }
        }

        _repository.SaveChanges();
        return item.Stock_Quantity;
    }

    public int IncreaseCartItems(string userEmail, int itemId)
    {
        return modifyCartItemQuantity(userEmail, itemId, '+');
    }

    public int DecreaseCartItems(string userEmail, int itemId)
    {
        return modifyCartItemQuantity(userEmail, itemId, '-');
    }
}