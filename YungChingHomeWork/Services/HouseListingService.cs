using YungChingHomeWork.Models;
using YungChingHomeWork.Repositories;
using System.Collections.Generic;

namespace YungChingHomeWork.Services
{
    public class HouseListingService
    {
        private readonly IHouseListingRepository _repository;

        public HouseListingService(IHouseListingRepository repository)
        {
            _repository = repository;
        }

        public HouseListing CreateHouseListing(HouseListing newListing)
        {
            return _repository.Create(newListing);
        }

        public bool UpdateHouseListing(int id, HouseListing updatedListing)
        {
            return _repository.Update(id, updatedListing);
        }

        public bool DeleteHouseListing(int id)
        {
            return _repository.Delete(id);
        }

        public List<HouseListing> GetAllHouseListings()
        {
            return _repository.GetAll();
        }

        public HouseListing? GetHouseListingById(int id)
        {
            return _repository.GetById(id);
        }
    }
}
