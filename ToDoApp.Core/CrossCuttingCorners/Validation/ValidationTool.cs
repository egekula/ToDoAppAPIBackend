using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Core.CrossCuttingCorners.Validation
{
    public static class ValidationTool
    {
        public static void Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var res = validator.Validate(context);
            if (!res.IsValid)
            {
                throw new ValidationException(res.Errors);
            }
        }
    }
}
