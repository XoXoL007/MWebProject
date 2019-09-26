using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using WebProject.Interfaces;

namespace WebProject.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly Model1 db = null;
        private readonly DbSet<T> table = null;

        public Repository()
        {
            db = new Model1();
            table = db.Set<T>();
        }

        public Repository(Model1 db)
        {
            this.db = db;
            table = db.Set<T>();
        }

        public void Create(T item)
        {
            table.Add(item);
        }

        public void Delete(int id)
        {
            T existingEntity = table.Find(id);
            table.Remove(existingEntity);
        }

        public T Get(int? id)
        {
            T entity = table.Find(id);
            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(T item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}