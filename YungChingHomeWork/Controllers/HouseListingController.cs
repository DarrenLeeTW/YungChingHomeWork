using Microsoft.AspNetCore.Mvc;
using YungChingHomeWork.Models;
using YungChingHomeWork.Services;
using System.Collections.Generic;
using System.Linq;
using NLog;

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
        /// 用於測試的記憶體內部售屋資料清單。
        /// </summary>
        private static List<HouseListing> houseListings = new List<HouseListing>();

        /// <summary>
        /// 售屋資料的唯一 ID 計數器。
        /// </summary>
        private static int nextId = 1;

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
            logger.Info("Creating a new house listing.");
            var createdListing = houseListingService.CreateHouseListing(newListing);
            return CreatedAtAction(nameof(GetHouseListingById), new { id = createdListing.Id }, createdListing);
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
            logger.Info($"Updating house listing with ID: {id}.");
            var success = houseListingService.UpdateHouseListing(id, updatedListing);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// 查詢所有售屋資料。
        /// </summary>
        /// <returns>所有售屋資料的清單。</returns>
        [HttpGet]
        public IActionResult GetAllHouseListings()
        {
            logger.Info("Retrieving all house listings.");
            var listings = houseListingService.GetAllHouseListings();
            return Ok(listings);
        }

        /// <summary>
        /// 根據 ID 查詢特定售屋資料。
        /// </summary>
        /// <param name="id">要查詢的售屋資料 ID。</param>
        /// <returns>若找到則返回售屋資料，否則返回 NotFound。</returns>
        [HttpGet("{id}")]
        public IActionResult GetHouseListingById(int id)
        {
            logger.Info($"Retrieving house listing with ID: {id}.");
            var listing = houseListingService.GetHouseListingById(id);
            if (listing == null)
            {
                return NotFound();
            }
            return Ok(listing);
        }
    }
}
