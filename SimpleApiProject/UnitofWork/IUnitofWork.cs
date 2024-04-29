using Microsoft.EntityFrameworkCore;
using SimpleApiProject.Services.ServiceInterfaces;

namespace SimpleApiProject.UnitofWork
{
    public interface IUnitofWork:IDisposable
    {
        public Task SaveChangesAsync();

    }


}
