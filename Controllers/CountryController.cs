using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Data;

namespace Hospital_Hub_API.Controllers
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly HospitalHubContext context;

        public CountryController(HospitalHubContext _context)
        {
            context = _context;
        }

        #region Get Country With Count Of State And City and Hospital
        [HttpGet]
        public IActionResult GetAllCountries()
        {
            var countries = context.HhCountries
                .Select(country => new
                {
                    country.CountryId,
                    country.CountryName,
                    countryCode = country.CountryName.Substring(0, 2).ToUpper(),
                    StateCount = context.HhStates.Count(s => s.CountryId == country.CountryId),
                    CityCount = context.HhCities.Count(c => c.State.CountryId == country.CountryId),
                    HospitalCount = context.HhHospitals
                        .Count(h => context.HhCities
                        .Any(c => c.CityId == h.CityId &&
                        context.HhStates.Any(s => s.StateId == c.StateId && s.CountryId == country.CountryId)
                )
            )
                }).ToList();

            return Ok(countries);
        }
        #endregion

        #region Export Countries to Excel
        [HttpGet]
        public IActionResult ExportToExcel()
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                var countries = context.HhCountries
                    .Select(country => new
                    {
                        CountryId = country.CountryId,
                        CountryName = country.CountryName,
                        StateCount = context.HhStates.Count(s => s.CountryId == country.CountryId),
                        CityCount = context.HhCities.Count(c => c.State.CountryId == country.CountryId),
                        HospitalCount = context.HhHospitals
                            .Count(h => context.HhCities
                            .Any(c => c.CityId == h.CityId &&
                            context.HhStates.Any(s => s.StateId == c.StateId && s.CountryId == country.CountryId)))
                    }).ToList();

                if (countries.Count == 0)
                    return BadRequest("No country data found.");

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Countries");
                    
                    // Add headers
                    worksheet.Cells["A1"].Value = "Country ID";
                    worksheet.Cells["B1"].Value = "Country Name";
                    worksheet.Cells["C1"].Value = "State Count";
                    worksheet.Cells["D1"].Value = "City Count";
                    worksheet.Cells["E1"].Value = "Hospital Count";

                    // Style headers
                    using (var range = worksheet.Cells["A1:E1"])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                    }

                    // Add data
                    for (int i = 0; i < countries.Count; i++)
                    {
                        worksheet.Cells[$"A{i + 2}"].Value = countries[i].CountryId;
                        worksheet.Cells[$"B{i + 2}"].Value = countries[i].CountryName;
                        worksheet.Cells[$"C{i + 2}"].Value = countries[i].StateCount;
                        worksheet.Cells[$"D{i + 2}"].Value = countries[i].CityCount;
                        worksheet.Cells[$"E{i + 2}"].Value = countries[i].HospitalCount;
                    }

                    // Auto-fit columns
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    var excelBytes = package.GetAsByteArray();

                    return File(excelBytes,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Countries_Data.xlsx");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error exporting to Excel: {ex.Message}");
            }
        }
        #endregion

        #region GetCountryById
        [HttpGet("{id}")]
        public IActionResult GetCountryById(int id)
        {
            var country = context.HhCountries.Find(id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }
        #endregion

        #region AddCountry
        [HttpPost]
        public IActionResult AddCountry([FromBody] HhCountry hhCountry)
        {
            if (hhCountry == null)
            {
                return BadRequest("Country data is null");
            }

            hhCountry.CreatedDate = DateTime.Now;
            hhCountry.ModifiedDate = null;

            context.HhCountries.Add(hhCountry);
            context.SaveChanges();

            return CreatedAtAction(nameof(GetCountryById), new { id = hhCountry.CountryId }, hhCountry);
        }
        #endregion

        #region UpdateCountry
        [HttpPut("{id}")]
        public IActionResult UpdateCountry(int id, [FromBody] HhCountry hhCountry)
        {
            if (id != hhCountry.CountryId)
            {
                return BadRequest("Country ID mismatch");
            }

            var existingCountry = context.HhCountries.Find(id);
            if (existingCountry == null)
            {
                return NotFound();
            }

            existingCountry.CountryName = hhCountry.CountryName;
            existingCountry.ModifiedDate = DateTime.Now;

            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region DeleteCountry
        [HttpDelete("{id}")]
        public IActionResult DeleteCountry(int id)
        {
            var country = context.HhCountries.Find(id);
            if (country == null)
            {
                return NotFound();
            }

            context.HhCountries.Remove(country);
            context.SaveChanges();

            return NoContent();
        }
        #endregion
    }
}
