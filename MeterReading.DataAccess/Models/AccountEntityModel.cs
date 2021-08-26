using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeterReading.DataAccess.Models
{
    [Table("Account")]
    public class AccountEntityModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
