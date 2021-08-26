using MeterReading.Logic.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MeterReading.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterReadingController : ControllerBase
    {
        private readonly IMeterReadingManager _meterReadingManager;
        public MeterReadingController(IMeterReadingManager meterReadingManager)
        {
            _meterReadingManager = meterReadingManager;
        }

        /// <summary>
        /// Get the list of data set associated with a program
        /// </summary>
        /// <param name="programId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("uploads")]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            var meterReadings = await _meterReadingManager.UploadMeterReadingFromCsv(filePath);

            return Ok(new { successfull_readings = meterReadings.Item1, failed_readings = meterReadings.Item2, filePath });
        }
    }
}
