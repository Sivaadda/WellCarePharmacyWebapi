using WellCarePharmacyWebapi.Models.Entities;

namespace WellCarePharmacyWebapi.Models.Repository.Interfaces
{
    public interface IOrdersRepository : IRepositoryBase<Order>
    {
        Task<IEnumerable<Order>> GetAllorders();

    }
}
