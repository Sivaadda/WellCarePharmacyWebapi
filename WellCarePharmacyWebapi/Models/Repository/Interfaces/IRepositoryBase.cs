namespace WellCarePharmacyWebapi.Models.Repository.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {

       Task<IEnumerable<T>> GetAll();

       Task<T> Create(T entity);

       Task Update(T entity);

       Task<T> Delete(int id);

       Task<T> GetById(int id);


    }
}

        