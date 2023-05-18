using Microsoft.EntityFrameworkCore;
using WellCarePharmacyWebapi.Models.Entities;
using WellCarePharmacyWebapi.Business_Logic_Layer.DTO;

namespace WellCarePharmacyWebapi.Models.Context
{
    public class WellCareDC:DbContext
    {
        public WellCareDC(DbContextOptions<WellCareDC> options) : base(options) { }

        public DbSet<Products> Products { get; set; }

        public DbSet<Orders> Orders { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var role1 = new Role { Id = 1, RoleName = "Admin" };
            var role2 = new Role { Id = 2, RoleName = "Customer" };

            modelBuilder.Entity<Role>().HasData(role1, role2);

            var product1 = new Products { Id = 7, ProductName = "Active Charcoal Soap", Descripition = "soap made with charcoal", Discount = 45, Status = "Available", ImageUrl = "iwyer" };
            var product2 = new Products { Id = 8, ProductName = "Active Soap", Descripition = "soap made with charcoal", Discount = 45, Status = "NotAvailable", ImageUrl = "iwyer" };

            var product3 = new Products { Id = 3, ProductName = "Active Charcoal Soap", Descripition = "soap made with charcoal", Discount = 45, Status = "Available", ImageUrl = "iwyer" };
            var product4 = new Products { Id = 4, ProductName = "Active Soap", Descripition = "soap made with charcoal", Discount = 45, Status = "NotAvailable", ImageUrl = "iwyer" };


            var product5 = new Products { Id = 5, ProductName = "Active Charcoal Soap", Descripition = "soap made with charcoal", Discount = 45, Status = "Available", ImageUrl = "iwyer" };
            var product6 = new Products { Id = 6, ProductName = "Active Soap", Descripition = "soap made with charcoal", Discount = 45, Status = "NotAvailable", ImageUrl = "iwyer" };
            modelBuilder.Entity<Products>().HasData(product1, product2, product3, product4, product5, product6);

            var user1 = new Users { Id = 1, Name = "siva", Email = "siva@gmail.com", Password = "12345", PhoneNumber = 99, RoleId=1};
            var user2 = new Users { Id = 2, Name = "siva", Email = "siva@gmail.com", Password = "12345", PhoneNumber = 99, RoleId=2 };
            modelBuilder.Entity<Users>().HasData(user1, user2);

            var order1 = new Orders { Id = 1, Quantity = 3, TotalPrice = 345 , ProductId=3,UsersId=1};
            modelBuilder.Entity<Orders>().HasData(order1);

            base.OnModelCreating(modelBuilder);
        }


    }
}
