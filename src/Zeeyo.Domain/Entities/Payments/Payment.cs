using Zeeyo.Domain.Commons;
using Zeeyo.Domain.Entities.Branches;
using Zeeyo.Domain.Entities.Students;

namespace Zeeyo.Domain.Entities.Payments;

public class Payment : Auditable
{
    public long StudentId { get; set; }
    public Student Student { get; set; }
    public long BranchId { get; set; }
    public Branch Branch { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}
