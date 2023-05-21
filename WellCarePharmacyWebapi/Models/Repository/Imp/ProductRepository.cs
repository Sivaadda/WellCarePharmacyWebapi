using WellCarePharmacyWebapi.Models.Context;
using WellCarePharmacyWebapi.Models.Entities;
using WellCarePharmacyWebapi.Models.Repository.Interfaces;

namespace WellCarePharmacyWebapi.Models.Repository.Imp
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(WellCareDC context): base(context) 
        
        {
            
        }
    }
}
