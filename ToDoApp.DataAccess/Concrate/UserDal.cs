using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Core.DataAccess.EntityFramework;
using ToDoApp.DataAccess.Abstract;
using ToDoApp.Entities.Concrate;

namespace ToDoApp.DataAccess.Concrate
{
    public class UserDal : EfEntityRepositoryBase<User>, IUserDal
    {
        public UserDal(DbContext context) : base(context)
        {
        }
    }
}
