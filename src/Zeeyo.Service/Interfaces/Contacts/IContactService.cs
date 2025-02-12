using Zeeyo.Service.DTOs.Contacts;

namespace Zeeyo.Service.Interfaces.Contacts;

public interface IContactService
{
    ContactDto Retrieve();
    Task<ContactDto> AddAsync(ContactDto dto);
}
