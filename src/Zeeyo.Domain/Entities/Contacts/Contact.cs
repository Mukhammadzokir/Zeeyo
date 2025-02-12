using Zeeyo.Domain.Commons;

namespace Zeeyo.Domain.Entities.Contacts;

public class Contact : Auditable
{
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }   
    public string Website { get; set; }
    public string Instagram { get; set; }
    public string Telegram { get; set; }
    public string LinkedIn { get; set; }
    public string Facebook { get; set; }
    public string Twitter { get; set; }
}
