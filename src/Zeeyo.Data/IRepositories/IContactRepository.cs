using Zeeyo.Domain.Entities.Contacts;

namespace Zeeyo.Data.IRepositories;

public interface IContactRepository : IRepository<Contact>
{
    public Contact Select();
}