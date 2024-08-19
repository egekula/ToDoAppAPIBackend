using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Entities.DTOs;

namespace ToDoApp.Business.ValidationRules.FluentValidation
{
    public class ToDoItemInsertValidator : AbstractValidator<ToDoItemInsertDto>
    {
        public ToDoItemInsertValidator()
        {
            RuleFor(todo => todo.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(5, 100).WithMessage("Title must be between 5 and 100 characters.");

            RuleFor(todo => todo.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(todo => todo.Priority)
                .InclusiveBetween(1, 5).WithMessage("Priority must be between 1 and 5.");

            RuleFor(todo => todo.DueDate)
                .GreaterThan(DateTime.Now).WithMessage("Due date must be in the future.")
                .When(todo => todo.DueDate.HasValue); // Only validate if DueDate is provided
        }
    }
}
