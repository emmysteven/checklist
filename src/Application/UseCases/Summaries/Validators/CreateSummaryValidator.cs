using Checklist.Application.UseCases.Summaries.Commands;
using FluentValidation;

namespace Checklist.Application.UseCases.Summaries.Validators;

public class CreateSummaryValidator : AbstractValidator<CreateSummaryCommand>
{

    public CreateSummaryValidator()
    {
        RuleFor(x => x.Makers)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} can not be empty")
            .NotNull();
        
        RuleFor(x => x.Checkers)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} can not be empty")
            .NotNull();
        
        RuleFor(x => x.Dbas)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} can not be empty")
            .NotNull();
        
        RuleFor(x => x.EodDate)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} can not be empty")
            .NotNull();
        
        RuleFor(x => x.StartTime)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} can not be empty")
            .NotNull();
        
        RuleFor(x => x.EndTime)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} can not be empty")
            .NotNull();
        
        RuleFor(x => x.Duration)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} can not be empty")
            .NotNull();
        
        RuleFor(x => x.TxnCount)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("{PropertyName} can not be empty")
            .NotNull();
        
    }
    
}