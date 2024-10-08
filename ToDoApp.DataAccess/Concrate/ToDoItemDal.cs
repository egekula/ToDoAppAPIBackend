﻿using System;
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
    public class ToDoItemDal : EfEntityRepositoryBase<ToDoItem>, IToDoItemDal
    {
        public ToDoItemDal(BackendContext context) : base(context)
        {
        }
    }
}
