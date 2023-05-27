namespace WellCarePharmacyWebapi.Infrastructure
{
    public static class Configuration
    {
        public static void RegisterProjectDependencies(this WebApplicationBuilder builder)
        {
          
            builder.Services.AddSwaggerGen();
        }
    }
}
