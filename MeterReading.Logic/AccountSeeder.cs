using CsvHelper;
using MeterReading.DataAccess;
using MeterReading.DataAccess.Models;
using System.Globalization;
using System.IO;
using System.Linq;
using static MeterReading.Logic.Helper.CsvHelper;

namespace MeterReading.Logic
{
    public class AccountSeeder
    {
        public static void Seed(RepositoryContext context, string filepath)
        {
            context.Database.EnsureCreated();

            if (context.Accounts.Any())
            {
                return;
            }
           
            using (StreamReader reader = new StreamReader(filepath))
            {
                CsvReader csvReader = new CsvReader(reader, CultureInfo.CurrentCulture);
                csvReader.Context.RegisterClassMap<AccountMapper>();
                var accounts = csvReader.GetRecords<AccountEntityModel>().ToArray().OrderBy(x => x.AccountId);
                context.Accounts.AddRange(accounts);
                context.SaveChanges();
            }
        }
    }
}
