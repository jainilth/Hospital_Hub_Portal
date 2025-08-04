    using Hospital_Hub_Portal.Models;
    using Microsoft.AspNetCore.Mvc;
    using OfficeOpenXml;
    //using Microsoft.EntityFrameworkCore;
    //using OfficeOpenXml;


namespace Hospital_Hub_API.Controllers
    {
    [Route("/api/[controller]/[action]")]

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

        #region Export Cities to Excel
        [HttpGet]
        public IActionResult ExportToExcel()
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                var cities = context.HhCities
                    .Select(city => new
                    {
                        CityId = city.CityId,
                        CityName = city.CityName,
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
                    }).ToList();

                if (cities.Count == 0)
                    return BadRequest("No city data found.");

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Cities");
                    
                    // Add headers
                    worksheet.Cells["A1"].Value = "City ID";
                    worksheet.Cells["B1"].Value = "City Name";
                    worksheet.Cells["C1"].Value = "State Name";
                    worksheet.Cells["D1"].Value = "Country Name";
                    worksheet.Cells["E1"].Value = "Hospital Count";

                    // Style headers
                    using (var range = worksheet.Cells["A1:E1"])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightCoral);
                    }

                    // Add data
                    for (int i = 0; i < cities.Count; i++)
                    {
                        worksheet.Cells[$"A{i + 2}"].Value = cities[i].CityId;
                        worksheet.Cells[$"B{i + 2}"].Value = cities[i].CityName;
                        worksheet.Cells[$"C{i + 2}"].Value = cities[i].StateName;
                        worksheet.Cells[$"D{i + 2}"].Value = cities[i].CountryName;
                        worksheet.Cells[$"E{i + 2}"].Value = cities[i].HospitalCount;
                    }

                    // Auto-fit columns
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    var excelBytes = package.GetAsByteArray();

                    return File(excelBytes,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Cities_Data.xlsx");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error exporting to Excel: {ex.Message}");
            }
        }
        #endregion

        #region GetCitiesByState
        [HttpGet("GetCitiesByState/{stateId}")]
        public IActionResult GetCitiesByState(int stateId)
        {
            var cities = context.HhCities
                .Where(c => c.StateId == stateId)
                .Select(c => new
                {
                    c.CityId,
                    c.CityName,
                    c.StateId
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
    }
}