using Microsoft.AspNetCore.Mvc;
using NLog;
using YungChingHomeWork.Models;
using YungChingHomeWork.Services;

namespace YungChingHomeWork.Controllers
{
    /// <summary>
    /// 控制器用於管理售屋資料。
    /// 提供新增、修改、查詢售屋資料的端點。
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class HouseListingController : ControllerBase
    {
        /// <summary>
        /// 日誌記錄器。
        /// </summary>
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 房屋列表服務。
        /// </summary>
        private readonly HouseListingService houseListingService;

        public HouseListingController(HouseListingService houseListingService)
        {
            this.houseListingService = houseListingService;
        }

        /// <summary>
        /// 新增售屋資料。
        /// </summary>
        /// <param name="newListing">要新增的售屋資料。</param>
        /// <returns>包含唯一 ID 的新增售屋資料。</returns>
        [HttpPost]
        public IActionResult CreateHouseListing([FromBody] HouseListing newListing)
        {
            try
            {
                if (newListing == null)
                {
                    return BadRequest("Request body is required.");
                }
                if (string.IsNullOrWhiteSpace(newListing.Name) || string.IsNullOrWhiteSpace(newListing.Address) || newListing.Price <= 0)
                {
                    return BadRequest("Invalid house listing data.");
                }
                logger.Info("Creating a new house listing.");
                var createdListing = houseListingService.CreateHouseListing(newListing);
                return CreatedAtAction(nameof(GetHouseListingById), new { id = createdListing.Id }, createdListing);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while creating a new house listing.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// 修改現有的售屋資料。
        /// </summary>
        /// <param name="id">要修改的售屋資料 ID。</param>
        /// <param name="updatedListing">更新後的售屋資料。</param>
        /// <returns>成功時返回 NoContent，若資料不存在則返回 NotFound。</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateHouseListing(int id, [FromBody] HouseListing updatedListing)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid id.");
                }
                if (updatedListing == null || string.IsNullOrWhiteSpace(updatedListing.Name) || string.IsNullOrWhiteSpace(updatedListing.Address) || updatedListing.Price <= 0)
                {
                    return BadRequest("Invalid house listing data.");
                }
                logger.Info($"Updating house listing with ID: {id}.");
                var success = houseListingService.UpdateHouseListing(id, updatedListing);
                if (!success)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Error occurred while updating house listing with ID: {id}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// 查詢所有售屋資料。
        /// </summary>
        /// <returns>所有售屋資料的清單。</returns>
        [HttpGet]
        public IActionResult GetAllHouseListings()
        {
            try
            {
                logger.Info("Retrieving all house listings.");
                var listings = houseListingService.GetAllHouseListings();
                if (listings == null || listings.Count == 0)
                {
                    return NotFound("No house listings found.");
                }
                return Ok(listings);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred while retrieving all house listings.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// 根據 ID 查詢特定售屋資料。
        /// </summary>
        /// <param name="id">要查詢的售屋資料 ID。</param>
        /// <returns>若找到則返回售屋資料，否則返回 NotFound。</returns>
        [HttpGet("{id}")]
        public IActionResult GetHouseListingById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid id.");
                }
                logger.Info($"Retrieving house listing with ID: {id}.");
                var listing = houseListingService.GetHouseListingById(id);
                if (listing == null)
                {
                    return NotFound("No house listings found.");
                }
                return Ok(listing);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Error occurred while retrieving house listing with ID: {id}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// 刪除售屋資料。
        /// </summary>
        /// <param name="id">要刪除的售屋資料 ID。</param>
        /// <returns>成功時返回 NoContent，若資料不存在則返回 NotFound。</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteHouseListing(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid id.");
                }
                logger.Info($"Deleting house listing with ID: {id}.");
                var success = houseListingService.DeleteHouseListing(id);
                if (!success)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Error occurred while deleting house listing with ID: {id}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
