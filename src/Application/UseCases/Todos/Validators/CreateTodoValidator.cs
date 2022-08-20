using Checklist.Application.Common.Interfaces;
using Checklist.Application.UseCases.Todos.Commands;
using FluentValidation;

namespace Checklist.Application.UseCases.Todos.Validators;

public class CreateTodoValidator : AbstractValidator<CreateTodoCommand>
{
    private readonly IDataContext _context;

    public CreateTodoValidator(IDataContext context)
    {
        _context = context;

        RuleFor(t => t.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} can not be empty")
            .NotNull()
            .Length(3, 50).WithMessage("{PropertyName} must be between 4 to 50 characters");
    }
    
}