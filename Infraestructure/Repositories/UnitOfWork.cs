using Application.Interfaces;
using Infraestructure.Context;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : AuditableEntity<int>
    {
        SQLDBContext context { get; set; }

        public UnitOfWork(SQLDBContext context)
        {
            this.context = context;
        }

        public DbSet<T> Entities => context.Set<T>();

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
