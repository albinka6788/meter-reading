using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeterReading.DataAccess.Models
{
    [Table("MeterReading")]
    public class MeterReadingEntityModel
    {
        public Guid Id { get; set; }
        public DateTime MeterReadingDateTime { get; set; }
        public int MeterReadValue { get; set; }

        [ForeignKey(nameof(AccountEntityModel))]
        public int AccountId { get; set; }
    }
}
