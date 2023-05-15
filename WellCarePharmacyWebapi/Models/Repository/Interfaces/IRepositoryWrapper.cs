namespace WellCarePharmacyWebapi.Models.Repository.Interfaces
{
    public interface IRepositoryWrapper
    {
         IProductRepository Products { get; }

         IUserRepository Users { get; }

         IOrdersRepository Orders { get; }

        void Save();

       
    }
}
