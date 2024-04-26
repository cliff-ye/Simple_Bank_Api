using Microsoft.EntityFrameworkCore;
using SimpleApiProject.Services.ServiceInterfaces;

namespace SimpleApiProject.UnitofWork
{
    public interface IUnitofWork:IDisposable
    {
        DbContext dbContext { get; }
        IAccount accountservice { get; }
        ITransaction transactionservice { get; }
        public Task SaveChangesAsync();

    }


}
