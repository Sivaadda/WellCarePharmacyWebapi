using Microsoft.AspNetCore.Http.Connections;
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


            var product1 = new Product
            {
                Id = 1,
                ProductName = "Blood Pressure Monitor",
                Price = 1950,
                Descripition = "Omron Fully Automatic Digital Blood Pressure",
                Discount = 27,
                Status = "Avaliable",
                ImageUrl = "https://tse2.mm.bing.net/th?id=OIP.EFGgzbJGFafyu3ySSQcqHgHaHa&pid=Api&P=0&h=180"
            };
            var product2 = new Product
            {
                Id = 2,
                ProductName = "Apollo Life  Fish Oil  ",
                Price = 200,
                Descripition = "Apollo life Fish oil Capsule contains Omega fatty acids,.",
                Discount = 10,
                Status = "Avaliable",
                ImageUrl = "https://newassets.apollo247.com/pub/media/catalog/product/a/p/apo0077-1.jpg"
            };
            var product3 = new Product
            {
                Id = 3,
                ProductName = "Apollo Life Cough Drops Lozengest",
                Price = 25,
                Descripition = "A cough drop is Ayurvedic Lozenges designed to deliver active .",
                Discount = 0,
                Status = "NotAvaliable",
                ImageUrl = "https://tse1.mm.bing.net/th?id=OIP.49yPPmnmItW7P3c3ffCuygHaHa&pid=Api&P=0&h=180"
            };
            modelBuilder.Entity<Product>().HasData(product1, product2, product3);

            modelBuilder.Entity<Role>().HasData(role1, role2);
           
            base.OnModelCreating(modelBuilder);
        }


    }
}
