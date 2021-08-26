using CsvHelper.Configuration;
using MeterReading.DataAccess.Models;
using MeterReading.Logic.Contracts;
using System;
using System.Globalization;

namespace MeterReading.Logic.Helper
{
    public class CsvHelper : ICsvHelper
    {
        public DateTime GetDateTimeCellValue(string inputValue, string cultureName)
        {
            var culture = CultureInfo.CreateSpecificCulture(cultureName);
            var styles = DateTimeStyles.None;
            DateTime value;
            if (DateTime.TryParse(inputValue, culture, styles, out value))
            {
                return value;
            }

            return DateTime.MinValue;
        }

        public int GetIntegerCellValue(string inputValue)
        {
            if (int.TryParse(inputValue, out int value))
            {
                return value;
            }
            return -1;
        }

        public sealed class AccountMapper : ClassMap<AccountEntityModel>
        {
            public AccountMapper()
            {
                AutoMap(CultureInfo.InvariantCulture);
                Map(m => m.Id).Ignore();
                Map(m => m.Id).Default(Guid.NewGuid());
            }
        }
    }
}
