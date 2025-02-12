using Zeeyo.Data.DbContexts;
using Zeeyo.Data.IRepositories;
using Zeeyo.Domain.Entities.Contacts;

namespace Zeeyo.Data.Repositories;

public class ContactRepository : Repository<Contact>, IContactRepository
{
    public ContactRepository(AppDbContext appDbContext) : base(appDbContext)
    {

    }
    public Contact Select()
    {
        var entity = _dbSet.FirstOrDefault();

        return entity;
    }
}
