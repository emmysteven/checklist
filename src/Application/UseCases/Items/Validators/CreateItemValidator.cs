using Checklist.Application.Common.Interfaces;
using Checklist.Application.UseCases.Items.Commands;
using FluentValidation;

namespace Checklist.Application.UseCases.Items.Validators;

public class CreateItemValidator : AbstractValidator<CreateItemCommand>
{
    private readonly IDataContext _context;

    public CreateItemValidator(IDataContext context)
    {
        _context = context;

        RuleFor(t => t.TodoId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} can not be empty")
            .NotNull();
    }
    
}