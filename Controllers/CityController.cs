    using Hospital_Hub_Portal.Models;
    using Microsoft.AspNetCore.Mvc;
    //using Microsoft.EntityFrameworkCore;
    //using OfficeOpenXml;


namespace Hospital_Hub_Portal.Controllers
    {
    [Route("/api/[controller]/[Action]")]

    [ApiController]
    public class CityController : Controller
    {
        private readonly HospitalHubContext context;

        public CityController(HospitalHubContext _context)
        {
            context = _context;
        }

        #region GetAll City with All the Count of the Hospital
        [HttpGet]
        public IActionResult GetAllCity()
        {
            var cities = context.HhCities
                .Select(city => new
                {
                    city.CityId,
                    city.CityName,

                    StateName = context.HhStates
                                    .Where(s => s.StateId == city.StateId)
                                    .Select(s => s.StateName)
                                    .FirstOrDefault(),

                    CountryName = context.HhCountries
                                    .Where(c => c.CountryId ==
                                        context.HhStates
                                            .Where(s => s.StateId == city.StateId)
                                            .Select(s => s.CountryId)
                                            .FirstOrDefault()
                                    )
                                    .Select(c => c.CountryName)
                                    .FirstOrDefault(),

                    HospitalCount = context.HhHospitals
                                        .Count(h => h.CityId == city.CityId)
                })
                .ToList();

            return Ok(cities);
        }
        #endregion

        #region GetCityById
        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var city = context.HhCities
                .Where(c => c.CityId == id)
                .Select(c => new
                {
                    c.CityId,
                    c.CityName,
                    c.StateId,
                    StateName = c.State.StateName,
                    CountryId = c.State.Country.CountryId,
                    CountryName = c.State.Country.CountryName
                })
                .FirstOrDefault();

            if (city == null)
                return NotFound("City not found");

            return Ok(city);
        }
        #endregion

        #region Add City
        [HttpPost]
        public IActionResult AddCity([FromBody] HhCity hhCity)
        {
            if (hhCity == null || string.IsNullOrWhiteSpace(hhCity.CityName) || hhCity.StateId == null)
            {
                return BadRequest("Invalid city data.");
            }

            hhCity.CreatedDate = DateTime.Now;
            hhCity.ModifiedDate = DateTime.Now;

            context.HhCities.Add(hhCity);
            context.SaveChanges();

            return Ok(hhCity);
        }
        #endregion

        #region Delet City
        [HttpDelete("{id}")]
        public IActionResult DeleteCity(int id)
            {
                var city = context.HhCities.FirstOrDefault(c => c.CityId ==  id);
                if (city == null)
                {
                    return NotFound();
                }
                context.HhCities.Remove(city);
                context.SaveChanges();
                return Ok(city);
            }
        #endregion


        #region Edit Citymo
        [HttpPut("{id}")]
        public IActionResult CityEdit(int id, [FromBody] HhCity hhCity)
        {
            if (hhCity == null || id != hhCity.CityId)
            {
                return BadRequest("Invalid city data or mismatched ID");
            }

            var existingCity = context.HhCities.Find(id);
            if (existingCity == null)
            {
                return NotFound();
            }

            existingCity.CityName = hhCity.CityName;
            existingCity.StateId = hhCity.StateId;
            existingCity.ModifiedDate = DateTime.Now;

            context.SaveChanges();

            return Ok(existingCity);
        }
        #endregion

        //#region Export Data In The Excel
        //[HttpGet("ExportToExcel")]
        //public IActionResult ExportToExcel()
        //{
        //    try
        //    {
        //        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        //        var cities = context.HhCities
        //            .Include(c => c.State)
        //            .ThenInclude(s => s.Country)
        //            .Select(c => new
        //            {
        //                CityId = c.CityId,
        //                CityName = c.CityName,
        //                StateName = c.State != null ? c.State.StateName : "N/A",
        //                CountryName = c.State != null && c.State.Country != null ? c.State.Country.CountryName : "N/A"
        //            }).ToList();

        //        if (cities.Count == 0)
        //            return BadRequest("No city data found.");

        //        using (var package = new ExcelPackage())
        //        {
        //            var worksheet = package.Workbook.Worksheets.Add("Cities");
        //            worksheet.Cells["A1"].LoadFromCollection(cities, true);
        //            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

        //            var excelBytes = package.GetAsByteArray();

        //            return File(excelBytes,
        //                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                "CityData.xlsx");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Error exporting to Excel: " + ex.Message + (ex.InnerException != null ? " | Inner: " + ex.InnerException.Message : ""));
        //    }
        //}
        //#endregion
    }
}