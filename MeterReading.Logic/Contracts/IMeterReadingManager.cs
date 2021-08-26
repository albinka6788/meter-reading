using System;
using System.Threading.Tasks;

namespace MeterReading.Logic.Contracts
{
    public interface IMeterReadingManager
    {
        Task<Tuple<int, int>> UploadMeterReadingFromCsv(string filePath);
    }
}