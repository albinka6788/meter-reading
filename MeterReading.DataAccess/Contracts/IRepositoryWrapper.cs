using System.Threading.Tasks;

namespace MeterReading.DataAccess.Contracts
{
    public interface IRepositoryWrapper
    {
        IAccountRepository AccountRepository { get; }
        IMeterReadingRepository MeterReadingRepository { get; }
        Task SaveAsync();
    }
}
