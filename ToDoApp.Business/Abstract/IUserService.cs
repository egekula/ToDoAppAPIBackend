using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Core.Results;
using ToDoApp.Entities.Concrate;
using ToDoApp.Entities.DTOs;

namespace ToDoApp.Business.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<UserDto>> GetByIdAsync(Guid id);
        Task<IDataResult<UserDto>> AddAsync(UserDto dto);

    }
}
