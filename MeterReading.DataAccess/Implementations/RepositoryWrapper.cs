using MeterReading.DataAccess.Contracts;
using System.Threading.Tasks;

namespace MeterReading.DataAccess.Implementations
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IMeterReadingRepository _meterReadingRepository;
        private IAccountRepository _accountRepository;

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public IAccountRepository AccountRepository
        {
            get
            {
                if (_accountRepository == null)
                {
                    _accountRepository = new AccountRepository(_repoContext);
                }
                return _accountRepository;
            }
        }

        public IMeterReadingRepository MeterReadingRepository
        {
            get
            {
                if (_meterReadingRepository == null)
                {
                    _meterReadingRepository = new MeterReadingRepository(_repoContext);
                }
                return _meterReadingRepository;
            }
        }

        public async Task SaveAsync()
        {
           await _repoContext.SaveChangesAsync();
        }
    }
}
