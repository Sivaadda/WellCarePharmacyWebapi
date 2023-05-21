using WellCarePharmacyWebapi.Models.Context;
using WellCarePharmacyWebapi.Models.Entities;
using WellCarePharmacyWebapi.Models.Repository.Interfaces;

namespace WellCarePharmacyWebapi.Models.Repository.Imp
{
    public class OrdersRepository: RepositoryBase<Order>, IOrdersRepository
    {
        public OrdersRepository(WellCareDC context) : base(context)
        {
        }

        
    }
}
