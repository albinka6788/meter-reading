using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeterReading.DataAccess.Models
{
    [Table("MeterReading")]
    public class MeterReadingEntityModel
    {
        [Required]
        public Guid Id { get; set; }
        public DateTime MeterReadingDateTime { get; set; }
        public int MeterReadValue { get; set; }

        [ForeignKey(nameof(AccountEntityModel))]
        [Required]
        public int AccountId { get; set; }
    }
}
