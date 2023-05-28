using Microsoft.EntityFrameworkCore;
using WellCarePharmacyWebapi.Models.Context;
using WellCarePharmacyWebapi.Models.Entities;
using WellCarePharmacyWebapi.Models.Repository.Interfaces;

namespace WellCarePharmacyWebapi.Models.Repository.Imp
{
    public class UserRepository: RepositoryBase<User>, IUserRepository
    {
        private readonly WellCareDC _context;
        public UserRepository(WellCareDC context) : base(context)

        {
            _context = context;

        }

        public async Task<IEnumerable<User>> GetAllusers()
        {
            return await _context.Set<User>().Include(o => o.Roles).ToListAsync();
        }
    }
}
