using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.DataAccess.Abstract;

namespace ToDoApp.Business.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserDal UserDal { get; }

        IToDoItemDal ToDoItemDal { get; }
        Task<int> CommitAsync();
        void Commit();
    }
}
