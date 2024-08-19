using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.DataAccess.Abstract;
using ToDoApp.DataAccess.Concrate;
using ToDoApp.DataAccess.Context;

namespace ToDoApp.Business.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BackendContext _context;
        public IUserDal UserDal { get; private set; }
        public IToDoItemDal ToDoItemDal { get; private set; }

        public UnitOfWork(BackendContext context)
        {
            _context = context;
            UserDal = new UserDal(_context);
            ToDoItemDal = new ToDoItemDal(_context);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            var transId = -1;
            if (_context.ChangeTracker.HasChanges())
            {
                using (var dbContextTransaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        if (_context != null)
                        {
                            transId = await _context.SaveChangesAsync();
                            dbContextTransaction.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw new Exception(ex.ToString());
                    }
                }
            }

            return transId;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_context == null) return;
            _context.Dispose();
        }
    }
}
