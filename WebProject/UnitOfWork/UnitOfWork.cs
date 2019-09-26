using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProject.Repository;

namespace WebProject.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Model1 db;
        private bool disposed = false;

        Repository<Authors> authorUowRepository;
        Repository<Books> bookUowRepository;

        public Repository<Authors> AuthorUowRepository
        {
            get
            {
                return authorUowRepository == null ? new Repository<Authors>(db) : authorUowRepository;
            }
        }

        public Repository<Books> BookUowRepository
        {
            get
            {
                return bookUowRepository == null ? new Repository<Books>(db) : bookUowRepository;
            }
        }

        public UnitOfWork()
        {
            db = new Model1();
        }

        public UnitOfWork(Model1 db)
        {
            this.db = db;
            db.Database.CommandTimeout = 180;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void BeginTransaction()
        {
            db.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            db.Database.CurrentTransaction.Commit();
        }
        protected virtual void Dispose(bool disposing)
        {
            if(this.disposed)
            {
                if(disposing)
                {
                    if(db != null)
                    {
                        db.Dispose();
                    }
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}