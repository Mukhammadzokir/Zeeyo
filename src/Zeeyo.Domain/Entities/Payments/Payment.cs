using Zeeyo.Domain.Commons;
using Zeeyo.Domain.Entities.Users;

namespace Zeeyo.Domain.Entities.Payments;

public class Payment : Auditable
{
    public long StudentId { get; set; }
    public User Student { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}
