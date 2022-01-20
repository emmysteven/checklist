using Checklist.Application.Common.Interfaces;
using Checklist.Application.UseCases.Todos.Commands;
using FluentValidation;

namespace Checklist.Application.UseCases.Todos.Validators;

public class CreateTodoValidator : AbstractValidator<CreateTodoCommand>
{
    private readonly ITodoRepository _todoRepository;

    public CreateTodoValidator(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;

        RuleFor(t => t.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} can not be empty")
            .NotNull()
            .Length(3, 50).WithMessage("{PropertyName} must be between 4 to 50 characters");
    }
    
}