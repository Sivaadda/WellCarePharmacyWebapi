using Microsoft.EntityFrameworkCore;
using WellCarePharmacyWebapi.Models.Entities;


namespace WellCarePharmacyWebapi.Models.Context
{
    public class WellCareDC:DbContext
    {
        public WellCareDC(DbContextOptions<WellCareDC> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var role1 = new Role { Id = 1, RoleName = "Admin" };
            var role2 = new Role { Id = 2, RoleName = "Customer" };

            modelBuilder.Entity<Role>().HasData(role1, role2);

            var product1 = new Product { Id = 7, ProductName = "Active Charcoal Soap", Descripition = "soap made with charcoal",Price=455, Discount = 45, Status = "Available", ImageUrl = "iwyer" };
            var product2 = new Product { Id = 8, ProductName = "Active Soap", Descripition = "soap made with charcoal", Price = 455, Discount = 45, Status = "NotAvailable", ImageUrl = "iwyer" };

            var product3 = new Product { Id = 3, ProductName = "Active Charcoal Soap", Descripition = "soap made with charcoal", Price = 455, Discount = 45, Status = "Available", ImageUrl = "iwyer" };
            var product4 = new Product { Id = 4, ProductName = "Active Soap", Descripition = "soap made with charcoal", Price = 455, Discount = 45, Status = "NotAvailable", ImageUrl = "iwyer" };


            var product5 = new Product { Id = 5, ProductName = "Active Charcoal Soap", Descripition = "soap made with charcoal", Price = 455, Discount = 45, Status = "Available", ImageUrl = "iwyer" };
            var product6 = new Product { Id = 6, ProductName = "Active Soap", Descripition = "soap made with charcoal", Price = 455, Discount = 45, Status = "NotAvailable", ImageUrl = "iwyer" };
            modelBuilder.Entity<Product>().HasData(product1, product2, product3, product4, product5, product6);

            var user1 = new User { Id = 1, Name = "siva", Email = "siva@gmail.com", Password = "12345678", PhoneNumber = "9700469909", RoleId=2};
            var user2 = new User { Id = 2, Name = "priya", Email = "priya@gmail.com", Password = "12345678", PhoneNumber = "9709876678", RoleId=2 };
            var user3 = new User { Id=3, Name ="admin", Email ="admin@gmail.com", Password="admin123", PhoneNumber ="9999999999", RoleId=1 };
            modelBuilder.Entity<User>().HasData(user1, user2, user3);

            var order1 = new Order { Id = 1, Quantity = 3, TotalPrice = 345 , ProductId=3,UsersId=1};
            var order2 = new Order { Id = 2, Quantity = 5, TotalPrice = 500, ProductId = 6, UsersId = 2 };
            modelBuilder.Entity<Order>().HasData(order1, order2);

            base.OnModelCreating(modelBuilder);
        }


    }
}
