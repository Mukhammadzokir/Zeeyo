using Zeeyo.Domain.Commons;

namespace Zeeyo.Domain.Entities.Messages;

public class Message : Auditable
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public string To { get; set; }
}
