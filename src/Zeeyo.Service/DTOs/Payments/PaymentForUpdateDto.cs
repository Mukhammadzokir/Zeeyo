namespace Zeeyo.Service.DTOs.Payments;

public class PaymentForUpdateDto
{
    public long StudentId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}
