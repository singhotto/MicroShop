using AutoMapper;
using Microsoft.Extensions.Logging;
using Warehouse.Repository;
using Warehouse.Repository.Model;
using Warehouse.Shared.Dto;

namespace Warehouse.Business.Kafka.MessageHandlers;

public class SupplierCateogiesKafkaMessageHandler : AbstractMessageHandler<CategoryDto, CategoryDto>
{
    private readonly IMapper _mapper;
    public SupplierCateogiesKafkaMessageHandler(ILogger<SupplierCateogiesKafkaMessageHandler> logger, IRepository repository, IMapper map) : base(logger, repository, map)
    {
        _mapper = map;
    }

    protected override void InsertDto(CategoryDto domainDto)
    {
        Category category = _mapper.Map<Category>(domainDto);
        Repository.Insert(category);
        Repository.SaveChanges();
    }
    protected override void UpdateDto(CategoryDto domainDto) {
        throw new NotImplementedException();
    }
    protected override void DeleteDto(CategoryDto domainDto) {
        throw new NotImplementedException();
    }
}
