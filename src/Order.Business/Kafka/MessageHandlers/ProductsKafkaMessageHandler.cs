using AutoMapper;
using Microsoft.Extensions.Logging;
using Order.Repository;
using Order.Repository.Model;
using Order.Shared.Dto;

namespace Order.Business.Kafka.MessageHandlers;

public class ProductsKafkaMessageHandler : AbstractMessageHandler<KafkaPaymentProductDto, ProductDto>
{
    private readonly IMapper _mapper;
    public ProductsKafkaMessageHandler(ILogger<ProductsKafkaMessageHandler> logger, IRepository repository, IMapper map) : base(logger, repository, map)
    {
        _mapper = map;
    }

    protected override void InsertDto(ProductDto domainDto)
    {
        Repository.CreateTransaction(() =>
        {
            Product product = _mapper.Map<Product>(domainDto);
            Console.WriteLine("ProductsKafkaMessageHandler");
            Repository.Insert(product);
            Repository.SaveChanges();
        });
    }
    protected override void UpdateDto(ProductDto domainDto) {
        throw new NotImplementedException();
    }
    protected override void DeleteDto(ProductDto domainDto) {
        throw new NotImplementedException();
    }
}
