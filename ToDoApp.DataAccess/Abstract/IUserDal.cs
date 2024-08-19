using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Core.DataAccess;
using ToDoApp.Entities.Concrate;

namespace ToDoApp.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
    }
}
