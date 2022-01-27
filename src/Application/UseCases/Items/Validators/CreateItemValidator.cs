using Checklist.Application.Common.Interfaces;
using Checklist.Application.UseCases.Items.Commands;
using FluentValidation;

namespace Checklist.Application.UseCases.Items.Validators;

public class CreateItemValidator : AbstractValidator<CreateItemCommand>
{
    private readonly IItemRepository _itemRepository;

    public CreateItemValidator(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;

        RuleFor(t => t.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} can not be empty")
            .NotNull()
            .Length(3, 50).WithMessage("{PropertyName} must be between 4 to 50 characters");
    }
    
}