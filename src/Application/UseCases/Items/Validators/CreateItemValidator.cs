using Checklist.Application.Common.Interfaces;
using Checklist.Application.UseCases.Items.Commands;
using FluentValidation;

namespace Checklist.Application.UseCases.Items.Validators;

public class CreateItemValidator : AbstractValidator<CreateItemCommand>
{
    private readonly IItemRepository _repo;

    public CreateItemValidator(IItemRepository repo)
    {
        _repo = repo;

        // RuleFor(t => t.Id)
        //     .Cascade(CascadeMode.Stop)
        //     .NotEmpty().WithMessage("{PropertyName} can not be empty")
        //     .NotNull();
    }
    
}