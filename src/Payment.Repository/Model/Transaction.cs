using System.ComponentModel.DataAnnotations.Schema;

namespace Payment.Repository.Model
{
    public class Transaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Payment_Id { get; set; }
        public required string User_Id { get; set; }
        public required String CardOwner { get; set; }
        public required String CardNumber { get; set; }
        public required int Expiry_month {  get; set; }
        public required int Expiry_year { get; set; }
        public required decimal Amount { get; set; }
        public DateTime ExecutionDate { get; set; } = DateTime.UtcNow;
        public string? Payment_Status { get; set; }
    }
}
