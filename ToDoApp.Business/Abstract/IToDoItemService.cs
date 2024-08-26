using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Core.Utilities.Results;
using ToDoApp.Entities.DTOs;

namespace ToDoApp.Business.Abstract
{
    public interface IToDoItemService
    {
        Task<IDataResult<ToDoItemInsertDto>> AddAsync(ToDoItemInsertDto dto);
        Task<IDataResult<List<ToDoItemDto>>> GetAllAsync();
        Task<IDataResult<ToDoItemDto>> GetByIdAsync(Guid id);
        Task<IResult> UpdateAsync(ToDoItemDto dto);
        Task<IResult> RemoveAsync(Guid id);
    }
}
