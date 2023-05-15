using Microsoft.EntityFrameworkCore;
using WellCarePharmacyWebapi.Models.Context;
using WellCarePharmacyWebapi.Models.Repository.Imp;
using WellCarePharmacyWebapi.Models.Repository.Interfaces;

namespace WellCarePharmacyWebapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IRepositoryWrapper,RepositoryWrapper>();  

            // Add services to the container.
            builder.Services.AddDbContext<WellCareDC>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("WCconnection")));

            builder.Services.AddCors(x => x.AddPolicy("corspolicy", build =>
                {
                    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
                }));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("corspolicy");
            app.UseCors(bulider =>
            {
                bulider
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
            });
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}