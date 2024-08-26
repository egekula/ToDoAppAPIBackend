using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Core.Entities.Concrate;
using ToDoApp.Core.Utilities.Results;
using ToDoApp.Entities.DTOs;

namespace ToDoApp.Business.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<List<OperationClaim>>> GetClaims(User user);
        Task<IDataResult<User>> GetByMail(string mail);
        Task<IDataResult<User>> AddAsync(User dto);

    }
}
