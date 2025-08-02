using YungChingHomeWork.Models;
using YungChingHomeWork.Repositories;
using System.Collections.Generic;
using System.Data.Common;
using NLog;

namespace YungChingHomeWork.Services
{
    public class HouseListingService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IHouseListingRepository _repository;

        public HouseListingService(IHouseListingRepository repository)
        {
            _repository = repository;
        }

        public HouseListing CreateHouseListing(HouseListing newListing)
        {
            try
            {
                return _repository.Create(newListing);
            }
            catch (DbException dbEx)
            {
                logger.Error(dbEx, "Database error in CreateHouseListing");
                throw new Exception("A database error occurred.", dbEx);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error in CreateHouseListing");
                throw;
            }
        }

        public bool UpdateHouseListing(int id, HouseListing updatedListing)
        {
            try
            {
                return _repository.Update(id, updatedListing);
            }
            catch (DbException dbEx)
            {
                logger.Error(dbEx, "Database error in UpdateHouseListing");
                throw new Exception("A database error occurred.", dbEx);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error in UpdateHouseListing");
                throw;
            }
        }

        public bool DeleteHouseListing(int id)
        {
            try
            {
                return _repository.Delete(id);
            }
            catch (DbException dbEx)
            {
                logger.Error(dbEx, "Database error in DeleteHouseListing");
                throw new Exception("A database error occurred.", dbEx);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error in DeleteHouseListing");
                throw;
            }
        }

        public List<HouseListing> GetAllHouseListings()
        {
            try
            {
                return _repository.GetAll();
            }
            catch (DbException dbEx)
            {
                logger.Error(dbEx, "Database error in GetAllHouseListings");
                throw new Exception("A database error occurred.", dbEx);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error in GetAllHouseListings");
                throw;
            }
        }

        public HouseListing? GetHouseListingById(int id)
        {
            try
            {
                return _repository.GetById(id);
            }
            catch (DbException dbEx)
            {
                logger.Error(dbEx, "Database error in GetHouseListingById");
                throw new Exception("A database error occurred.", dbEx);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error in GetHouseListingById");
                throw;
            }
        }
    }
}
