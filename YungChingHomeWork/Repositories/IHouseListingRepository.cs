using YungChingHomeWork.Models;
using System.Collections.Generic;

namespace YungChingHomeWork.Repositories
{
    public interface IHouseListingRepository
    {
        HouseListing Create(HouseListing houseListing);
        bool Update(int id, HouseListing houseListing);
        bool Delete(int id);
        List<HouseListing> GetAll();
        HouseListing? GetById(int id);
    }
}
