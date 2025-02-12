using AutoMapper;
using Zeeyo.Data.IRepositories;
using Zeeyo.Service.DTOs.Contacts;
using Zeeyo.Domain.Entities.Contacts;
using Zeeyo.Service.Interfaces.Contacts;

namespace Zeeyo.Service.Services.Contacts;

public class ContactSerivce : IContactService
{
    private IMapper _mapper;
    private readonly IContactRepository _repository;

    public ContactSerivce(IMapper mapper,IContactRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public ContactDto Retrieve()
    {
        return _mapper.Map<ContactDto>(_repository.Select());
    }

    public async Task<ContactDto> AddAsync(ContactDto dto)
    {
        var contact = _repository.Select();

        if (contact != null)
        {
            contact.Address = dto.Address;
            contact.Phone = dto.Phone;
            contact.Email = dto.Email;
            contact.Website = dto.Website;
            contact.Instagram = dto.Instagram;
            contact.Telegram = dto.Telegram;
            contact.LinkedIn = dto.LinkedIn;
            contact.Facebook = dto.Facebook;
            contact.Twitter = dto.Twitter;

            var result = await _repository.UpdateAsync(contact);
            return _mapper.Map<ContactDto>(result);
        }
        else
        {
            var result = await _repository.InsertAsync(_mapper.Map<Contact>(dto));
            return _mapper.Map<ContactDto>(result);
        }
    }
}
