using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Core.DataAccess.EntityFramework;
using ToDoApp.Core.Entities.Concrate;
using ToDoApp.DataAccess.Abstract;
using ToDoApp.DataAccess.Context;

namespace ToDoApp.DataAccess.Concrate
{
    public class UserDal : EfEntityRepositoryBase<User>, IUserDal
    {
        private readonly BackendContext _context;

        public UserDal(BackendContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<OperationClaim>> GetClaims(User user)
        {
            
                var result = from operationClaim in _context.OperationClaims
                             join userOperationClaim in _context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();
        }
    }
}
