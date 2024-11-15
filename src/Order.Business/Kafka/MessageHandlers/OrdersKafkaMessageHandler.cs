using AutoMapper;
using Microsoft.Extensions.Logging;
using Order.Repository;
using Order.Repository.Model;
using Order.Shared.Dto;

namespace Order.Business.Kafka.MessageHandlers;

public class OrdersKafkaMessageHandler : AbstractMessageHandler<KafkaOrderInsertDto, KafkaOrderInsertDto>
{
    private readonly IMapper _mapper;
    public OrdersKafkaMessageHandler(ILogger<OrdersKafkaMessageHandler> logger, IRepository repository, IMapper map) : base(logger, repository, map)
    {
        _mapper = map;
    }

    protected override void InsertDto(KafkaOrderInsertDto domainDto)
    {
        Repository.CreateTransaction(() =>
        {
            Orders order = _mapper.Map<Orders>(domainDto);

            if (domainDto.Products != null && domainDto.Products.Any())
            {
                order.Order_Status = "Order Created";
                order.Tracking_Number = "Pending";
                Repository.Insert(order);
                Repository.SaveChanges();

                List<OrderProductList> productOrders = new List<OrderProductList>();
                
                foreach(KafkaProductInsertDto product in domainDto.Products)
                {
                    productOrders.Add(new OrderProductList() { 
                        Order_Id = order.Order_Id,
                        Product_Id = product.Product_Id,
                        Stock_Quantity = product.Stock_Quantity,
                    });
                }
                Repository.InsertProductsToOrder(productOrders);
                Repository.SaveChanges();
            }
        });
    }
    protected override void UpdateDto(KafkaOrderInsertDto domainDto) {
        throw new NotImplementedException();
    }
    protected override void DeleteDto(KafkaOrderInsertDto domainDto) {
        throw new NotImplementedException();
    }
}
