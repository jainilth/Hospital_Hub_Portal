using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Hub_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalReviewController : Controller
    {
        private readonly HospitalHubContext context;
        public HospitalReviewController(HospitalHubContext _context)
        {
            context = _context;
        }

        #region GetAllHospitalReviews
        [HttpGet]
        public IActionResult GetAllHospitalReviews()
        {
            var reviews = context.HhHospitalReviews.ToList();
            return Ok(reviews);
        }
        #endregion

        #region GetHospitalReviewById
        [HttpGet("{id}")]
        public IActionResult GetHospitalReviewById(int id)
        {
            var review = context.HhHospitalReviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }
        #endregion

        #region AddHospitalReview
        [HttpPost]
        public IActionResult AddHospitalReview([FromBody] HhHospitalReview hhHospitalReview)
        {
            if (hhHospitalReview == null)
            {
                return BadRequest("Hospital review data is null");
            }
            hhHospitalReview.CreatedDate = DateTime.Now;
            hhHospitalReview.ModifiedDate = null;
            context.HhHospitalReviews.Add(hhHospitalReview);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetHospitalReviewById), new { id = hhHospitalReview.ReviewId }, hhHospitalReview);
        }
        #endregion

        #region UpdateHospitalReview
        [HttpPut("{id}")]
        public IActionResult UpdateHospitalReview(int id, [FromBody] HhHospitalReview hhHospitalReview)
        {
            if (hhHospitalReview == null || hhHospitalReview.ReviewId != id)
            {
                return BadRequest("Hospital review data is null or ID mismatch");
            }
            var existingReview = context.HhHospitalReviews.Find(id);
            if (existingReview == null)
            {
                return NotFound();
            }
            existingReview.Rating = hhHospitalReview.Rating;
            existingReview.ReviewText = hhHospitalReview.ReviewText;
            existingReview.ModifiedDate = DateTime.Now;
            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region DeleteHospitalReview
        [HttpDelete("{id}")]
        public IActionResult DeleteHospitalReview(int id)
        {
            var review = context.HhHospitalReviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }
            context.HhHospitalReviews.Remove(review);
            context.SaveChanges();
            return NoContent();
        }
        #endregion

    }
}
