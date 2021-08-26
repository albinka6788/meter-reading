using System;
using System.Collections.Generic;
using System.Text;

namespace MeterReading.Logic.Contracts
{
    public interface ICsvHelper
    {
        int GetIntegerCellValue(string inputValue);
        DateTime GetDateTimeCellValue(string inputValue, string culture);
    }
}
