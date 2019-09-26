using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Repository;

namespace WebProject.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Repository<Authors> AuthorUowRepository { get; }
        Repository<Books> BookUowRepository { get; }

        void Save();
        void BeginTransaction();
        void CommitTransaction();

    }
}
