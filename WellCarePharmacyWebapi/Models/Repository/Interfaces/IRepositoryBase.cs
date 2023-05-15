namespace WellCarePharmacyWebapi.Models.Repository.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        IEnumerable<T> GetAll();
      
        void Create(T entity);
        void  Update(T entity);

        void Delete(int id);
        T GetById(int id);

        void Save();
    }
    }

        