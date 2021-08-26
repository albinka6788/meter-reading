using MeterReading.DataAccess.Contracts;
using MeterReading.DataAccess.Models;

namespace MeterReading.DataAccess.Implementations
{
    public class AccountRepository : RepositoryBase<AccountEntityModel>, IAccountRepository
    {
        public AccountRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
