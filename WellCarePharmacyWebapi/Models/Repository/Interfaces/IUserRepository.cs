using WellCarePharmacyWebapi.Business_Logic_Layer.DTO;
using WellCarePharmacyWebapi.Models.Entities;

namespace WellCarePharmacyWebapi.Models.Repository.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<IEnumerable<User>> GetAllusers();
        Task<User> GetuserById(int id);

        

    }
}
