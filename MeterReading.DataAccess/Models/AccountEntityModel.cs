using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeterReading.DataAccess.Models
{
    [Table("Account")]
    public class AccountEntityModel
    {
        public Guid Id { get; set; }
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
