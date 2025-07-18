    using Hospital_Hub_Portal.Models;
    using Microsoft.AspNetCore.Mvc;

    namespace Hospital_Hub_Portal.Controllers
    {
        [Route("/api/[controller]")]

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

            #region GetCityByID
            [HttpGet("{id}")]
            public IActionResult GetCityByID(int id)
            {
                var city = context.HhCities.Find(id);
                if (city == null)
                {
                    return BadRequest();
                }
                return Ok(city);
            }
            #endregion

            #region AddCity
            [HttpPost]
            public IActionResult AddCity([FromBody] HhCity hhCity)
            {
                if (hhCity == null)
                {
                    return BadRequest("City data is null");
                }

                hhCity.CreatedDate = DateTime.Now;
                hhCity.ModifiedDate = null;

                context.HhCities.Add(hhCity);
                context.SaveChanges();

                return CreatedAtAction(nameof(GetCityByID), new { id = hhCity.CityId }, hhCity);
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

        //#region Edit City
        //[HttpPost]
        //public async Task<IActionResult> AddEdit(HhCity hhCity)
        //{

        //}
    }
}
