using Microsoft.EntityFrameworkCore;
using WellCarePharmacyWebapi.Models.Context;
using WellCarePharmacyWebapi.Models.Repository.Interfaces;

namespace WellCarePharmacyWebapi.Models.Repository.Imp
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {

        private readonly WellCareDC _context;

        private readonly DbSet<T> dbset;

        public RepositoryBase(WellCareDC context)
        {
            _context = context;
            dbset= _context.Set<T>();
        }
        public void Create(T entity)
        {
           dbset.Add(entity);
        }

        public void Delete(int id)
        {
            var entity= dbset.Find(id);
            dbset.Remove(entity);
                       
            
        }

        public IEnumerable<T> GetAll()
        {
           return _context.Set<T>().ToList(); 
        }

      

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public T GetById(int id)
        {
            return dbset.Find(id);

        }
    }
}
