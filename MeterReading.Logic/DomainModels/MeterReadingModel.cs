using System;

namespace MeterReading.Logic.DomainModels
{
    public class MeterReadingModel
    {
        public int AccountId { get; set; }
        public DateTime MeterReadingDateTime { get; set; }
        public int MeterReadValue { get; set; }
    }
}
