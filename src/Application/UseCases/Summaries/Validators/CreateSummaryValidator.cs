using Checklist.Application.Common.Interfaces;
using Checklist.Application.UseCases.Summaries.Commands;
using FluentValidation;

namespace Checklist.Application.UseCases.Summaries.Validators;

public class CreateSummaryValidator : AbstractValidator<CreateSummaryCommand>
{
    private readonly ISummaryRepository _repo;

    public CreateSummaryValidator(ISummaryRepository repo)
    {
        _repo = repo;

        // RuleFor(t => t.Maker)
        //     .Cascade(CascadeMode.Stop)
        //     .NotEmpty().WithMessage("{PropertyName} can not be empty")
        //     .NotNull();
    }
    
}