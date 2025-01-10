using Zeeyo.Domain.Entities.Users;
using Zeeyo.Domain.Entities.Branches;

namespace Zeeyo.Service.DTOs.Payments;

public class PaymentForResultDto
{
    public long Id { get; set; }
    public long StudentId { get; set; }
    public User Student { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}
