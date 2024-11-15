using AutoMapper;
using Payment.Business.Factory;
using Payment.Repository;
using Payment.Repository.Model;
using Payment.Shared.Dto;

namespace Payment.Business;

public class Business : IBusiness
{

    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    public Business(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public TransactionDto AddTransaction(TransactionOrderInsertDto transaction)
    {
        Transaction t = _mapper.Map<Transaction>(transaction);

        //Process payment
        t.Payment_Status = "Payment_OK";

        OrderDto order = _mapper.Map<OrderDto>(transaction);

        _repository.CreateTransaction(() => {
            _repository.Insert(t);
            _repository.SaveChanges();

            _repository.InsertTransactionalOutbox(TransactionalOutboxFactory.CreateInsert(order));

            _repository.SaveChanges();
        });
        return _mapper.Map<TransactionDto>(t);
    }

    public List<TransactionDto> GetAllTransactions(string user_id = "", DateTime from = default, DateTime to = default)
    {
        if (from == default)
            from = new DateTime(2022, 01, 01);

        if (to == default)
            to = DateTime.Now;

        IQueryable<Transaction> query = _repository.ReadTrancations()
            .Where(o => o.ExecutionDate >= from && o.ExecutionDate <= to);

        if (!string.IsNullOrEmpty(user_id))
        {
            query = query.Where(o => o.User_Id == user_id);
        }

        return _mapper.Map<List<TransactionDto>>(query.ToList());    
    }

    public TransactionDto? GetTransaction(int transactionId)
    {
        return _mapper.Map<TransactionDto>(_repository.GetTransaction(transactionId));
    }

    public List<TransactionDto> GetTransactions()
    {
        return _mapper.Map<List<TransactionDto>>(_repository.ReadTrancations().ToList());
    }
}