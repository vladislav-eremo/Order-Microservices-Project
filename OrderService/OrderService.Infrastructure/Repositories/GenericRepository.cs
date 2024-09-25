using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Repositories
{
    public class GenericRepository<T>(OrderServiceDBContext db) : IGenericRepository<T> where T : class
    {
        private DbSet<T> DBSet => db.Set<T>();

        public void Add(T entity)
        {
            DBSet.Add(entity);
        }

        public void Delete(T entity)
        {
            DBSet.Remove(entity);
        }

        public IQueryable<T> Get()
        {
            return DBSet.AsQueryable().AsNoTracking();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(T entity)
        {
            DBSet.Update(entity);
        }
    }
}
