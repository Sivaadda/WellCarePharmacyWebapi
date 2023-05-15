using WellCarePharmacyWebapi.Models.Context;
using WellCarePharmacyWebapi.Models.Entities;
using WellCarePharmacyWebapi.Models.Repository.Interfaces;

namespace WellCarePharmacyWebapi.Models.Repository.Imp
{
    public class UserRepository: RepositoryBase<Users>, IUserRepository
    {
        public UserRepository(WellCareDC context) : base(context)

        {

        }
    }
}
