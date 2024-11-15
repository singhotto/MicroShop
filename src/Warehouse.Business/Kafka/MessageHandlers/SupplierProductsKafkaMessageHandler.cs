using AutoMapper;
using Microsoft.Extensions.Logging;
using Warehouse.Business.Factory;
using Warehouse.Repository;
using Warehouse.Repository.Model;
using Warehouse.Shared.Dto;

namespace Warehouse.Business.Kafka.MessageHandlers;

public class SupplierProductsKafkaMessageHandler : AbstractMessageHandler<ProductDto, ProductDto>
{
    private readonly IMapper _mapper;
    public SupplierProductsKafkaMessageHandler(ILogger<SupplierProductsKafkaMessageHandler> logger, IRepository repository, IMapper map) : base(logger, repository, map)
    {
        _mapper = map;
    }

    protected override void InsertDto(ProductDto domainDto)
    {
        Repository.CreateTransaction(() =>
        {
            Product product = _mapper.Map<Product>(domainDto);
            Repository.Insert(product);
            Repository.SaveChanges();


            Repository.InsertTransactionalOutbox(TransactionalOutboxFactory.CreateInsert(_mapper.Map<ProductDto>(product)));
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
