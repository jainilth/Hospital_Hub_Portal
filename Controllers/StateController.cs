using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace Hospital_Hub_API.Controllers
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class StateController : Controller
    {
        private readonly HospitalHubContext context;
        public StateController(HospitalHubContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var country = context.HhStates.ToList();
            return Ok(country);
        }

        #region GetState List And Their Country and Count Of City and Hospital
        [HttpGet]
        public IActionResult GetALlSatate()
        {
            var states = context.HhStates
                    .Select(state => new
                    {
                        state.StateId,
                        state.StateName,
                        CountryName = context.HhCountries
                                .Where(c => c.CountryId == state.CountryId)
                                .Select(c => c.CountryName).FirstOrDefault(),
                        CityCount = context.HhCities.Count(city => city.StateId == state.StateId),
                        HospitalCount = context.HhHospitals.Count(h => h.StateId == state.StateId)
                    })
        .ToList();
            return Ok(states);
        }
        #endregion

        #region Export States to Excel
        [HttpGet]
        public IActionResult ExportToExcel()
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                var states = context.HhStates
                    .Select(state => new
                    {
                        StateId = state.StateId,
                        StateName = state.StateName,
                        CountryName = context.HhCountries
                            .Where(c => c.CountryId == state.CountryId)
                            .Select(c => c.CountryName).FirstOrDefault(),
                        CityCount = context.HhCities.Count(city => city.StateId == state.StateId),
                        HospitalCount = context.HhHospitals.Count(h => h.StateId == state.StateId)
                    }).ToList();

                if (states.Count == 0)
                    return BadRequest("No state data found.");

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("States");
                    
                    // Add headers
                    worksheet.Cells["A1"].Value = "State ID";
                    worksheet.Cells["B1"].Value = "State Name";
                    worksheet.Cells["C1"].Value = "Country Name";
                    worksheet.Cells["D1"].Value = "City Count";
                    worksheet.Cells["E1"].Value = "Hospital Count";

                    // Style headers
                    using (var range = worksheet.Cells["A1:E1"])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);
                    }

                    // Add data
                    for (int i = 0; i < states.Count; i++)
                    {
                        worksheet.Cells[$"A{i + 2}"].Value = states[i].StateId;
                        worksheet.Cells[$"B{i + 2}"].Value = states[i].StateName;
                        worksheet.Cells[$"C{i + 2}"].Value = states[i].CountryName;
                        worksheet.Cells[$"D{i + 2}"].Value = states[i].CityCount;
                        worksheet.Cells[$"E{i + 2}"].Value = states[i].HospitalCount;
                    }

                    // Auto-fit columns
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    var excelBytes = package.GetAsByteArray();

                    return File(excelBytes,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "States_Data.xlsx");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error exporting to Excel: {ex.Message}");
            }
        }
        #endregion


        #region GetStateById
        [HttpGet("{id}")]
        public IActionResult GetStateById(int id)
        {
            var state = context.HhStates.Find(id);
            if (state == null)
            {
                return NotFound();
            }

            var stateWithCounts = new
            {
                state.StateId,
                state.StateName,
                state.CountryId,
                state.CreatedDate,
                state.ModifiedDate,
                CountryName = context.HhCountries
                    .Where(c => c.CountryId == state.CountryId)
                    .Select(c => c.CountryName).FirstOrDefault(),
                CityCount = context.HhCities.Count(city => city.StateId == state.StateId),
                HospitalCount = context.HhHospitals.Count(h => h.StateId == state.StateId)
            };

            return Ok(stateWithCounts);
        }
        #endregion

        #region AddState
        [HttpPost]
        public IActionResult AddState([FromBody] HhState hhState)
        {
            if (hhState == null)
            {
                return BadRequest("State data is null");
            }
            hhState.CreatedDate = DateTime.Now;
            hhState.ModifiedDate = null;
            context.HhStates.Add(hhState);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetStateById), new { id = hhState.StateId }, hhState);
        }
        #endregion

        #region UpdateState
        [HttpPut("{id}")]
        public IActionResult UpdateState(int id, [FromBody] HhState hhState)
        {
            if (hhState == null || hhState.StateId != id)
            {
                return BadRequest("State data is null or ID mismatch");
            }
            var existingState = context.HhStates.Find(id);
            if (existingState == null)
            {
                return NotFound();
            }
            existingState.StateName = hhState.StateName;
            existingState.ModifiedDate = DateTime.Now;
            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region DeleteState
        [HttpDelete("{id}")]
        public IActionResult DeleteState(int id)
        {
            var state = context.HhStates.Find(id);
            if (state == null)
            {
                return NotFound();
            }
            context.HhStates.Remove(state);
            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region GetStaetByCountry
        [HttpGet("GetStatesByCountry/{countryId}")]
        public IActionResult GetStatesByCountry(int countryId)
        {
            var states = context.HhStates
                .Where(s => s.CountryId == countryId)
                .Select(s => new {
                    stateId = s.StateId,
                    stateName = s.StateName
                })
                .ToList();

            return Ok(states);
        }
        #endregion
    }
}
