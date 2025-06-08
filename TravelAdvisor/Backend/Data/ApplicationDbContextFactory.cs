using Microsoft.EntityFrameworkCore;

namespace TravelAdvisor.Backend.Data
{
    public class ApplicationDbContextFactory : IDbContextFactory<ApplicationDbContext>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public ApplicationDbContextFactory(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public ApplicationDbContext CreateDbContext()
        {
            return _factory.CreateDbContext();
        }
    }
} 