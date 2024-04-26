using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleApiProject.Data;
using SimpleApiProject.Models;
using SimpleApiProject.Services.ServiceInterfaces;

namespace SimpleApiProject.UnitofWork
{
    public class UnitofWork : IUnitofWork
    {
        private readonly AppDbContext _appDbContext;
        public UnitofWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public DbContext dbContext => _appDbContext;

        public IAccount accountservice { get; set; }

        public ITransaction transactionservice { get; set; }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            AddTimestamps();
            await _appDbContext.SaveChangesAsync();
        }

        private void AddTimestamps()
        {
            var entities = _appDbContext.ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow; // current datetime

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = now;
                }
                ((BaseEntity)entity.Entity).UpdatedAt = now;
            }
        }
        }
}
