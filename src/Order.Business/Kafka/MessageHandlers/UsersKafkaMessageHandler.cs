using AutoMapper;
using Microsoft.Extensions.Logging;
using Order.Repository;
using Order.Repository.Model;

namespace Order.Business.Kafka.MessageHandlers;

public class UsersKafkaMessageHandler : AbstractMessageHandler<User, User>
{
    private readonly IMapper _mapper;
    public UsersKafkaMessageHandler(ILogger<UsersKafkaMessageHandler> logger, IRepository repository, IMapper map) : base(logger, repository, map)
    {
        _mapper = map;
    }

    protected override void InsertDto(User domainDto)
    {
        for (int i = 0; i < 10; i++)
            Console.WriteLine("**** User: " + domainDto.User_Id + "*****");
        Repository.Insert(domainDto);
        Repository.SaveChanges();
    }
    protected override void UpdateDto(User domainDto) {
        throw new NotImplementedException();
    }
    protected override void DeleteDto(User domainDto) {
        throw new NotImplementedException();
    }
}
