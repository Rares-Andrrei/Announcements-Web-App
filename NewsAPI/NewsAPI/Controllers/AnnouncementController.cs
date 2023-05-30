using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using NewsAPI.Dto;
using NewsAPI.Model;
using NewsAPI.Services;
using System.ComponentModel.DataAnnotations;

namespace NewsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnnouncementController : ControllerBase
    {
        IAnnouncementCollectionService _announcementCollectionService;
        public AnnouncementController(IAnnouncementCollectionService announcementCollectionService)
        {
            _announcementCollectionService = announcementCollectionService ?? throw new ArgumentNullException(nameof(AnnouncementCollectionService));
        }


        /// <summary>
        /// Get all the announcements
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAnnouncements()
        {
            List<Announcement> announcements = await _announcementCollectionService.GetAll();
            return Ok(announcements);
        }


        /// <summary>
        /// Create an announcement
        /// </summary>
        /// <param name="announcement"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAnnouncement([FromBody] AnnouncementDto announcement)
        {
            if (announcement == null)
            {
                return BadRequest("Announcement cannot be null");
            }
            await _announcementCollectionService.Create(new Announcement { 
                Id = Guid.NewGuid(),
                Author = announcement.Author, 
                CategoryId = announcement.CategoryId, 
                Message = announcement.Message,
                ImageUrl = announcement.ImageUrl,
                Title = announcement.Title
            });
            return Ok(announcement);
        }

        /// <summary>
        /// Delete an existing announcement
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAnnouncement(Guid id)
        {
            if (id == null)
            {
                return BadRequest("Id cannot be null");
            }
            if (await _announcementCollectionService.Delete(id))
            {
                return Ok();
            }
            return NotFound();
        }
        /// <summary>
        /// Update an existing announcement
        /// </summary>
        /// <param name="announcement"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnnouncement([Required] Guid id, [FromBody] Announcement announcement)
        {
            if (announcement == null)
            {
                return BadRequest("Announcement cannot be null");
            }
            if (await _announcementCollectionService.Update(id, announcement))
            {
                return Ok();
            }
            return NotFound();           
        }

        /// <summary>
        /// Get an announcement by it's Id
        /// </summary>
        /// <param name="id">Announcement Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnnouncementById(Guid id)
        {
            var announcement = await _announcementCollectionService.Get(id);

            if (announcement == null)
            {
                return BadRequest("Announcement not found!");
            }
            return Ok(announcement);
        }

    }
}
