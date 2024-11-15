using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Supply.Business.Factory;
using Supply.Client.Http.Abstractions;
using Supply.Repository;
using Supply.Repository.Model;
using Supply.Shared.Dto;

namespace Supply.Business;

public class Business : IBusiness
{

    private readonly ISupplyClient _supplyClient;
    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    private static int rand = 0;
    public Business(IRepository repository, IMapper mapper, ISupplyClient supplyClient)
    {
        _repository = repository;
        _mapper = mapper;
        _supplyClient = supplyClient;
    }
    public OrderDto AddOrder(string supplierId, List<OrderProductInsertDto> productList)
    {
        Order order = new Order() { User_Id = supplierId };
        List<ProductOrderList> productOrders = new List<ProductOrderList>();
        _repository.CreateTransaction(() =>
        {
            _repository.Insert(order);
            _repository.SaveChanges();


            foreach (OrderProductInsertDto product in productList)
            {
                productOrders.Add(new ProductOrderList()
                {
                    Order_Id = order.Order_Id,
                    Product_Id = product.Product_Id,
                    Stock_Quantity = product.Stock_Quantity
                });
            }

            _repository.Insert(productOrders);

            _repository.SaveChanges();
        });
        Console.WriteLine(order.Order_Id + " " + order.User_Id + " ");
        OrderDto ord = new OrderDto()
        {
            Order_Id = order.Order_Id,
            User_Id = order.User_Id,
            Date = order.Date,
            Status = order.Status,
            Tracking_Number = order.Tracking_Number
        };
        //ord.Products = _mapper.Map<List<ProductOrderDto>>(productOrders);
        return ord;
    }

    public void AddProducts(List<ProductInsertDto> products)
    {
        foreach(ProductInsertDto product in products)
        {
            Product p = _mapper.Map<Product>(product);
            if (!_repository.isAlready(p))
                _repository.Insert(p);
        }

        _repository.SaveChanges();
    }

    public void AddRandomProducts(string supplierId)
    {
        if (rand >= 20) return;

        if (rand == 0) AddRandomCategories();

        List<ProductInsertDto> products = GetRandomProducts(supplierId);
        AddProducts(products);
        rand += 10;
    }

    public async Task<SupplierDto?> AddSupplier(string supplierEmail, string accessToken)
    {
        SupplierDto? supplier =  await _supplyClient.AddSupplierHttp(supplierEmail, accessToken);
        if (supplier != null)
        {
            _repository.Insert(_mapper.Map<Supplier>(supplier));
            _repository.SaveChanges();
        }
        return supplier;
    }

    public void DeleteOrder(int id)
    {
        _repository.DeleteOrder(id);
    }

    public void DeleteSupplier(string id)
    {
        _repository.DeleteSupplier(id);
    }

    public OrderDto? GetOrder(int id)
    {
        Order? o = _repository.GetOrderById(id);

        if (o == null) return null;

        return _mapper.Map<OrderDto>(o);
    }

    public List<OrderProductDto> GetOrderOfSupplier(string supplierId, string status = "", DateTime from = default, DateTime to = default)
    {
        if (from == default)
            from = new DateTime(2022, 01, 01);

        if (to == default)
            to = DateTime.Now;

        IQueryable<Order> query = _repository.ReadOrder()
            .Include(o => o.Products)
                .ThenInclude(p => p.Product)
            .Where(o => o.User_Id == supplierId && o.Date >= from && o.Date <= to);

        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(o => o.Status == status);
        }

        List<OrderProductDto> result = query
            .Select(o => new OrderProductDto
            {
                Order_Id = o.Order_Id,
                User_Id = o.User_Id,
                Status = o.Status,
                Date = o.Date,
                Tracking_Number = o.Tracking_Number,
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

    public List<ProductDto>? GetProductOfSupplier(string supplierId)
    {
        Supplier? supplier = _repository.GetSupplierById(supplierId);

        if(supplier == null) return null;

        List<Product>? products = supplier.Products;

        return _mapper.Map<List<ProductDto>>(products);
    }

    public List<ProductDto> GetProducts()
    {
        return _repository.ReadProduct().AsEnumerable().Select(_mapper.Map<ProductDto>).ToList();
    }

    public SupplierDto? GetSupplier(string id)
    {
        Supplier? s = _repository.GetSupplierById(id);

        if (s == null) return null;

        return _mapper.Map<SupplierDto>(s);
    }

    public List<SupplierDto> GetSuppliers()
    {
        // Include the Products when reading suppliers
        var suppliersWithProducts = _repository.ReadSupplier().Include(s => s.Products).ToList();

        // Map the entities to SupplierDto
        return suppliersWithProducts.Select(s => _mapper.Map<SupplierDto>(s)).ToList();
    }

    public OrderDto? UpdateOrder(int id, OrderDto order)
    {
        return _mapper.Map<OrderDto>(_repository.UpdateOrder(id, _mapper.Map<Order>(order)));
    }

    public OrderDto? UpdateOrderStatus(int id, string status)
    {
        OrderDto order = _mapper.Map<OrderDto>(_repository.UpdateOrderStatus(id, status));

        if(status.ToLower() == "delivered")
        {
            Order o = _repository.ReadOrder()
            .Include(o => o.Products)
                .ThenInclude(p => p.Product).FirstOrDefault(x => x.Order_Id == id);

            List<KafkaProductInsertDto> products = new List<KafkaProductInsertDto>();

            foreach(ProductOrderList productList in o.Products){
                KafkaProductInsertDto kf = _mapper.Map<KafkaProductInsertDto>(productList.Product);
                kf.Stock_Quantity = productList.Stock_Quantity;
                _repository.InsertTransactionalOutbox(TransactionalOutboxFactory.CreateInsert(kf));
            }
            _repository.SaveChanges();
        }

        return order;
    }

    public SupplierDto? UpdateSupplier(string id, SupplierDto supplier)
    {

        return _mapper.Map<SupplierDto>(_repository.UpdateSupplier(id, _mapper.Map<Supplier>(supplier)));
    }

    private void AddRandomCategories()
    {
        Category[] categories =
        {
            new Category() { Category_Name = "men's clothing" },
            new Category() { Category_Name = "jewelery" },
            new Category() { Category_Name = "electronics" },
            new Category() { Category_Name = "women's clothing" }
        };

        if (_repository.isAlready(categories[0])) return;
        _repository.CreateTransaction(() =>
        {
            foreach(Category category in categories)
            {
                _repository.Insert(category);
            }
            _repository.SaveChanges();

            foreach(Category category in categories)
            {
                _repository.InsertTransactionalOutbox(TransactionalOutboxFactory.CreateInsert(_mapper.Map<CategoryDto>(category)));
            }
            _repository.SaveChanges();
        });
    }

    private List<ProductInsertDto> GetRandomProducts(string supplierId)
    {
        string jsonProducts = @"[
              {
                Name: 'Fjallraven - Foldsack No. 1 Backpack, Fits 15 Laptops',
                Description: 'Your perfect pack for everyday use and walks in the forest. Stash your laptop (up to 15 inches) in the padded sleeve, your everyday',
                Category: ""men's clothing"",
                Image: 'https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_.jpg',
                Price: 109.95
              },
              {
                Name: 'Mens Casual Premium Slim Fit T-Shirts ',
                Description: 'Slim-fitting style, contrast raglan long sleeve, three-button henley placket, light weight & soft fabric for breathable and comfortable wearing. And Solid stitched shirts with round neck made for durability and a great fit for casual fashion wear and diehard baseball fans. The Henley style round neckline includes a three-button placket.',
                Category: ""men's clothing"",
                Image: 'https://fakestoreapi.com/img/71-3HjGNDUL._AC_SY879._SX._UX._SY._UY_.jpg',
                Price: 22.3
              },
              {
                Name: 'Mens Cotton Jacket',
                Description: 'great outerwear jackets for Spring/Autumn/Winter, suitable for many occasions, such as working, hiking, camping, mountain/rock climbing, cycling, traveling or other outdoors. Good gift choice for you or your family member. A warm hearted love to Father, husband or son in this thanksgiving or Christmas Day.',
                Category: ""men's clothing"",
                Image: 'https://fakestoreapi.com/img/71li-ujtlUL._AC_UX679_.jpg',
                Price: 55.99
              },
              {
                Name: 'Mens Casual Slim Fit',
                Description: 'The color could be slightly different between on the screen and in practice. / Please note that body builds vary by person, therefore, detailed size information should be reviewed below on the product description.',
                Category: ""men's clothing"",
                Image: 'https://fakestoreapi.com/img/71YXzeOuslL._AC_UY879_.jpg',
                Price: 15.99
              },
              {
                Name: ""John Hardy Women's Legends Naga Gold & Silver Dragon Station Chain Bracelet"",
                Description: ""From our Legends Collection, the Naga was inspired by the mythical water dragon that protects the ocean's pearl. Wear facing inward to be bestowed with love and abundance, or outward for protection."",
                Category: 'jewelery',
                Image: 'https://fakestoreapi.com/img/71pWzhdJNwL._AC_UL640_QL65_ML3_.jpg',
                Price: 695
              },
              {
                Name: 'Solid Gold Petite Micropave ',
                Description: 'Satisfaction Guaranteed. Return or exchange any order within 30 days.Designed and sold by Hafeez Center in the United States. Satisfaction Guaranteed. Return or exchange any order within 30 days.',
                Category: 'jewelery',
                Image: 'https://fakestoreapi.com/img/61sbMiUnoGL._AC_UL640_QL65_ML3_.jpg',
                Price: 168
              },
              {
                Name: 'White Gold Plated Princess',
                Description: ""Classic Created Wedding Engagement Solitaire Diamond Promise Ring for Her. Gifts to spoil your love more for Engagement, Wedding, Anniversary, Valentine's Day..."",
                Category: 'jewelery',
                Image: 'https://fakestoreapi.com/img/71YAIFU48IL._AC_UL640_QL65_ML3_.jpg',
                Price: 9.99
              },
              {
                Name: 'Pierced Owl Rose Gold Plated Stainless Steel Double',
                Description: 'Rose Gold Plated Double Flared Tunnel Plug Earrings. Made of 316L Stainless Steel',
                Category: 'jewelery',
                Image: 'https://fakestoreapi.com/img/51UDEzMJVpL._AC_UL640_QL65_ML3_.jpg',
                Price: 10.99
              },
              {
                Name: 'WD 2TB Elements Portable External Hard Drive - USB 3.0 ',
                Description: 'USB 3.0 and USB 2.0 Compatibility Fast data transfers Improve PC Performance High Capacity; Compatibility Formatted NTFS for Windows 10, Windows 8.1, Windows 7; Reformatting may be required for other operating systems; Compatibility may vary depending on user’s hardware configuration and operating system',
                Category: 'electronics',
                Image: 'https://fakestoreapi.com/img/61IBBVJvSDL._AC_SY879_.jpg',
                Price: 64
              },
              {
                Name: 'SanDisk SSD PLUS 1TB Internal SSD - SATA III 6 Gb/s',
                Description: 'Easy upgrade for faster boot up, shutdown, application load and response (As compared to 5400 RPM SATA 2.5” hard drive; Based on published specifications and internal benchmarking tests using PCMark vantage scores) Boosts burst write performance, making it ideal for typical PC workloads The perfect balance of performance and reliability Read/write speeds of up to 535MB/s/450MB/s (Based on internal testing; Performance may vary depending upon drive capacity, host device, OS and application.)',
                Category: 'electronics',
                Image: 'https://fakestoreapi.com/img/61U7T1koQqL._AC_SX679_.jpg',
                Price: 109
              },
              {
                Name: 'Silicon Power 256GB SSD 3D NAND A55 SLC Cache Performance Boost SATA III 2.5',
                Description: '3D NAND flash are applied to deliver high transfer speeds Remarkable transfer speeds that enable faster bootup and improved overall system performance. The advanced SLC Cache Technology allows performance boost and longer lifespan 7mm slim design suitable for Ultrabooks and Ultra-slim notebooks. Supports TRIM command, Garbage Collection technology, RAID, and ECC (Error Checking & Correction) to provide the optimized performance and enhanced reliability.',
                Category: 'electronics',
                Image: 'https://fakestoreapi.com/img/71kWymZ+c+L._AC_SX679_.jpg',
                Price: 109
              },
              {
                Name: 'WD 4TB Gaming Drive Works with Playstation 4 Portable External Hard Drive',
                Description: ""Expand your PS4 gaming experience, Play anywhere Fast and easy, setup Sleek design with high capacity, 3-year manufacturer's limited warranty"",
                Category: 'electronics',
                Image: 'https://fakestoreapi.com/img/61mtL65D4cL._AC_SX679_.jpg',
                Price: 114
              },
              {
                Name: 'Acer SB220Q bi 21.5 inches Full HD (1920 x 1080) IPS Ultra-Thin',
                Description: '21. 5 inches Full HD (1920 x 1080) widescreen IPS display And Radeon free Sync technology. No compatibility for VESA Mount Refresh Rate: 75Hz - Using HDMI port Zero-frame design | ultra-thin | 4ms response time | IPS panel Aspect ratio - 16: 9. Color Supported - 16. 7 million colors. Brightness - 250 nit Tilt angle -5 degree to 15 degree. Horizontal viewing angle-178 degree. Vertical viewing angle-178 degree 75 hertz',
                Category: 'electronics',
                Image: 'https://fakestoreapi.com/img/81QpkIctqPL._AC_SX679_.jpg',
                Price: 599
              },
              {
                Name: 'Samsung 49-Inch CHG90 144Hz Curved Gaming Monitor (LC49HG90DMNXZA) – Super Ultrawide Screen QLED ',
                Description: '49 INCH SUPER ULTRAWIDE 32:9 CURVED GAMING MONITOR with dual 27 inch screen side by side QUANTUM DOT (QLED) TECHNOLOGY, HDR support and factory calibration provides stunningly realistic and accurate color and contrast 144HZ HIGH REFRESH RATE and 1ms ultra fast response time work to eliminate motion blur, ghosting, and reduce input lag',
                Category: 'electronics',
                Image: 'https://fakestoreapi.com/img/81Zt42ioCgL._AC_SX679_.jpg',
                Price: 999.99
              },
              {
                Name: ""BIYLACLESEN Women's 3-in-1 Snowboard Jacket Winter Coats"",
                Description: 'Note:The Jackets is US standard size, Please choose size as your usual wear Material: 100% Polyester; Detachable Liner Fabric: Warm Fleece. Detachable Functional Liner: Skin Friendly, Lightweigt and Warm.Stand Collar Liner jacket, keep you warm in cold weather. Zippered Pockets: 2 Zippered Hand Pockets, 2 Zippered Pockets on Chest (enough to keep cards or keys)and 1 Hidden Pocket Inside.Zippered Hand Pockets and Hidden Pocket keep your things secure. Humanized Design: Adjustable and Detachable Hood and Adjustable cuff to prevent the wind and water,for a comfortable fit. 3 in 1 Detachable Design provide more convenience, you can separate the coat and inner as needed, or wear it together. It is suitable for different season and help you adapt to different climates',
                Category: ""women's clothing"",
                Image: 'https://fakestoreapi.com/img/51Y5NI-I5jL._AC_UX679_.jpg',
                Price: 56.99
              },
              {
                Name: ""Lock and Love Women's Removable Hooded Faux Leather Moto Biker Jacket"",
                Description: '100% POLYURETHANE(shell) 100% POLYESTER(lining) 75% POLYESTER 25% COTTON (SWEATER), Faux leather material for style and comfort / 2 pockets of front, 2-For-One Hooded denim style faux leather jacket, Button detail on waist / Detail stitching at sides, HAND WASH ONLY / DO NOT BLEACH / LINE DRY / DO NOT IRON',
                Category: ""women's clothing"",
                Image: 'https://fakestoreapi.com/img/81XH0e8fefL._AC_UY879_.jpg',
                Price: 29.95
              },
              {
                Name: 'Rain Jacket Women Windbreaker Striped Climbing Raincoats',
                Description: ""Lightweight perfet for trip or casual wear---Long sleeve with hooded, adjustable drawstring waist design. Button and zipper front closure raincoat, fully stripes Lined and The Raincoat has 2 side pockets are a good size to hold all kinds of things, it covers the hips, and the hood is generous but doesn't overdo it.Attached Cotton Lined Hood with Adjustable Drawstrings give it a real styled look."",
                Category: ""women's clothing"",
                Image: 'https://fakestoreapi.com/img/71HblAHs5xL._AC_UY879_-2.jpg',
                Price: 39.99
              },
              {
                Name: ""MBJ Women's Solid Short Sleeve Boat Neck V "",
                Description: '95% RAYON 5% SPANDEX, Made in USA or Imported, Do Not Bleach, Lightweight fabric with great stretch for comfort, Ribbed on sleeves and neckline / Double stitching on bottom hem',
                Category: ""women's clothing"",
                Image: 'https://fakestoreapi.com/img/71z3kpMAYsL._AC_UY879_.jpg',
                Price: 9.85
              },
              {
                Name: ""Opna Women's Short Sleeve Moisture"",
                Description: '100% Polyester, Machine wash, 100% cationic polyester interlock, Machine Wash & Pre Shrunk for a Great Fit, Lightweight, roomy and highly breathable with moisture wicking fabric which helps to keep moisture away, Soft Lightweight Fabric with comfortable V-neck collar and a slimmer fit, delivers a sleek, more feminine silhouette and Added Comfort',
                Category: ""women's clothing"",
                Image: 'https://fakestoreapi.com/img/51eg55uWmdL._AC_UX679_.jpg',
                Price: 7.95
              },
              {
                Name: 'DANVOUY Womens T Shirt Casual Cotton Short',
                Description: '95%Cotton,5%Spandex, Features: Casual, Short Sleeve, Letter Print,V-Neck,Fashion Tees, The fabric is soft and has some stretch., Occasion: Casual/Office/Beach/School/Home/Street. Season: Spring,Summer,Autumn,Winter.',
                Category: ""women's clothing"",
                Image: 'https://fakestoreapi.com/img/61pHAEJ4NML._AC_UX679_.jpg',
                Price: 12.99
              }
            ]";

        List<ProductInsertAutorized> allProducts = JsonConvert.DeserializeObject<List<ProductInsertAutorized>>(jsonProducts);

        List<ProductInsertDto> products = new List<ProductInsertDto>();

        for(int i = rand; i< rand + 10; i++)
        {
            Random random = new Random();

            // Generate a random number between 0 and 19
            int randomInRange = random.Next(0, 20);
            products.Add(new ProductInsertDto()
            {
                Name = allProducts[randomInRange].Name,
                Description = allProducts[randomInRange].Description,
                Category_Id = _repository.ReadCategory().FirstOrDefault(x => x.Category_Name == allProducts[randomInRange].Category).Category_Id,
                Price = allProducts[randomInRange].Price,
                Image = allProducts[randomInRange].Image,
                Supplier_Id = supplierId
            });
        }

        return products;
    }
}