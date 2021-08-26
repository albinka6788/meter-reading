using MeterReading.DataAccess.Contracts;

namespace MeterReading.DataAccess.Implementations
{
    public class MeterReadingRepository : RepositoryBase<Models.MeterReadingEntityModel>, IMeterReadingRepository
    {
        public MeterReadingRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
