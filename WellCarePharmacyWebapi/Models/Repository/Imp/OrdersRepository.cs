using Microsoft.EntityFrameworkCore;
using System.Linq;
using WellCarePharmacyWebapi.Models.Context;
using WellCarePharmacyWebapi.Models.Entities;
using WellCarePharmacyWebapi.Models.Repository.Interfaces;

namespace WellCarePharmacyWebapi.Models.Repository.Imp
{
    public class OrdersRepository: RepositoryBase<Order>, IOrdersRepository
    {
        private readonly WellCareDC _context;
        public OrdersRepository(WellCareDC context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllorders()
        {
            return await _context.Set<Order>().Include(o => o.Products).Include(o => o.Users).ThenInclude(u => u.Roles).ToListAsync();
        }

        public async Task<Order> Getorderbyid(int id)
        {
            return await _context.Set<Order>().Include(o => o.Products).Include(o => o.Users).ThenInclude(u => u.Roles).FirstOrDefaultAsync(u => u.Id == id);
        }

    }

}
