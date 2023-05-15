using WellCarePharmacyWebapi.Models.Context;
using WellCarePharmacyWebapi.Models.Repository.Interfaces;

namespace WellCarePharmacyWebapi.Models.Repository.Imp
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private IProductRepository _Products;

        private IUserRepository _User;

        private IOrdersRepository _Orders;

        private WellCareDC _wellcarecontext;

       
        public RepositoryWrapper( WellCareDC wellcarecontext)
        {
             _wellcarecontext = wellcarecontext;
       

        }

        public IProductRepository Products
        {
            get
            {
                if (_Products == null)
                {
                    _Products = new ProductRepository(_wellcarecontext);
                }
                return _Products;
            }

           
        }

        public IOrdersRepository Orders
        {
            get
            {
                if (_Orders == null)
                {
                    _Orders = new OrdersRepository(_wellcarecontext);
                }
                return _Orders;
            }


        }

        public IUserRepository Users
        {
            get
            {
                if (_User == null)
                {
                    _User = new UserRepository(_wellcarecontext);
                }
                return _User;
            }

        }

        public void Save()
        {
            _wellcarecontext.SaveChanges();

        }
    }
}
