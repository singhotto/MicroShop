using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payment.Business;
using Payment.Shared.Dto;

namespace Payment.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PaymentController : ControllerBase
    {

        private readonly IBusiness _business;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IBusiness business, ILogger<PaymentController> logger)
        {
            _business = business;
            _logger = logger;
        }

        [HttpPost("/Payment/ProcessPayment", Name = "ProcessPayment")]
        [Authorize]
        public ActionResult ProcessPayment(TransactionOrderInsertDto transaction)
        {
            transaction.User_Id = User.Identity.Name;
            return Ok(_business.AddTransaction(transaction));
        }

        [HttpGet(Name = "GetPayment")]
        [Authorize(Roles = "Admin")]
        public ActionResult<TransactionDto> GetPayment(int payment_id)
        {
            return Ok(_business.GetTransaction(payment_id));
        }

        [HttpGet(Name = "PaymentHistory")]
        [Authorize(Roles = "Admin")]
        public ActionResult<List<TransactionDto>> PaymentHistory(string user_id, DateTime from, DateTime to)
        {
            return Ok(_business.GetAllTransactions(user_id, from, to));
        }
    }
}
