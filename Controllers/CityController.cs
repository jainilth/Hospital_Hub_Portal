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
        public IActionResult GetCityById(int id)
        {
            var city = context.HhCities
                .Where(c => c.CityId == id)
                .Select(c => new
                {
                    cityId = c.CityId,
                    cityName = c.CityName,
                    stateId = c.StateId,
                    countryId = c.State != null ? c.State.CountryId : null
                })
                .FirstOrDefault();

            if (city == null)
                return NotFound();

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

        //#region Edit City
        //[HttpPost]
        //public async Task<IActionResult> AddEdit(HhCity hhCity)
        //{

        //}
            //#endregion
        //}
        //#endregion

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
