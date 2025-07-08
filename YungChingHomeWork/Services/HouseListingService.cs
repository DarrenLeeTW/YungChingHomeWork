using YungChingHomeWork.Models;
using System.Collections.Generic;
using System.Linq;

namespace YungChingHomeWork.Services
{
    public class HouseListingService
    {
        private static List<HouseListing> houseListings = new List<HouseListing>();
        private static int nextId = 1;

        public HouseListing CreateHouseListing(HouseListing newListing)
        {
            newListing.Id = nextId++;
            houseListings.Add(newListing);
            return newListing;
        }

        public bool UpdateHouseListing(int id, HouseListing updatedListing)
        {
            var existingListing = houseListings.FirstOrDefault(h => h.Id == id);
            if (existingListing == null)
            {
                return false;
            }

            existingListing.Name = updatedListing.Name;
            existingListing.Address = updatedListing.Address;
            existingListing.Price = updatedListing.Price;
            return true;
        }

        public List<HouseListing> GetAllHouseListings()
        {
            return houseListings;
        }

        public HouseListing GetHouseListingById(int id)
        {
            return houseListings.FirstOrDefault(h => h.Id == id);
        }
    }
}
