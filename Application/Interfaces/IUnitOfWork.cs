using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUnitOfWork<T> where T: class
    {
        public DbSet<T> Entities { get; }

        public Task SaveAsync();
    }
}
