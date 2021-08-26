using MeterReading.DataAccess.Contracts;
using MeterReading.DataAccess.Models;
using MeterReading.Logic.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MeterReading.Logic.Implementation
{
    public class MeterReadingManager : IMeterReadingManager
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ICsvHelper _csvHelper;
        private readonly ILogger _logger;
        public MeterReadingManager(IRepositoryWrapper repositoryWrapper, ICsvHelper csvHelper, ILogger logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _csvHelper = csvHelper;
            _logger = logger;
        }

        /// <summary>
        /// Read the csv file and map it to the Meter reading model entity
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<Tuple<int, int>> UploadMeterReadingFromCsv(string filePath)
        {
            int success = 0;
            int failed = 0;
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                int index = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] columnData = line.Split(new char[] { ',' });
                    index += 1;

                    // Skip first row which in this case is a header with column names
                    if (index <= 1)
                        continue;

                    MeterReadingEntityModel meterReadingEntity = new MeterReadingEntityModel();

                    // validate the account level row
                    int accountId = _csvHelper.GetIntegerCellValue(columnData[0]);
                    if (accountId == -1)
                    {
                        _logger.LogWarning("Account Id is not a valid Integer input");
                        failed++;
                        continue;
                    }
                    meterReadingEntity.AccountId = accountId;

                    // validate the meter reading time level row
                    DateTime meterReadingTime = _csvHelper.GetDateTimeCellValue(columnData[1], "en-GB");
                    if (meterReadingTime == DateTime.MinValue)
                    {
                        _logger.LogWarning("Meter Reading Date is not a valid DateTime");
                        failed++;
                        continue;
                    }
                    meterReadingEntity.MeterReadingDateTime = meterReadingTime;

                    // validate the meter reading level row
                    int meterReadingValue = _csvHelper.GetIntegerCellValue(columnData[2]);
                    if (meterReadingValue == -1 || meterReadingValue.ToString().Length != 4)
                    {
                        _logger.LogWarning("Meter Reading Value is not a valid Integer input or not in the format NNNN");
                        failed++;
                        continue;
                    }
                    meterReadingEntity.MeterReadValue = meterReadingValue;

                    // Check the account exists before inserting the meter reading for an account
                    var account = await _repositoryWrapper.AccountRepository.FindByCondition(x => x.AccountId == meterReadingEntity.AccountId)
                                                                        .FirstOrDefaultAsync();

                    if (account == null)
                    {
                        _logger.LogWarning("Account does not exist for the meter reading");
                        failed++;
                        continue;
                    }

                    // Add the reading in the DB if the account Id does not exist
                    var meterReading = await _repositoryWrapper.MeterReadingRepository.FindByCondition(x => x.AccountId == meterReadingEntity.AccountId)
                                                                                 .FirstOrDefaultAsync();
                    if (meterReading == null)
                    {
                        meterReadingEntity.Id = Guid.NewGuid();
                        _repositoryWrapper.MeterReadingRepository.Create(meterReadingEntity);
                        await _repositoryWrapper.SaveAsync();
                    }
                    success++;
                }
            }
            return new Tuple<int, int>(success, failed);
        }
    }
}
