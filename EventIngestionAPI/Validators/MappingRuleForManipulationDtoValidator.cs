using EventIngestionAPI.ApiModels;
using FluentValidation;

namespace EventIngestionAPI.Validators;

public class MappingRuleForManipulationDtoValidator<T> : AbstractValidator<T> where T : MappingRuleForManipulationDto
{
    public MappingRuleForManipulationDtoValidator()
    {
        RuleFor(mr => mr.ExternalField)
            .NotEmpty().WithMessage("ExternalField is required.")
            .MaximumLength(100).WithMessage("ExternalField must not exceed 100 characters.");

        RuleFor(mr => mr.InternalField)
            .NotEmpty().WithMessage("InternalField is required.")
            .MaximumLength(100).WithMessage("InternalField must not exceed 100 characters.");

        RuleFor(mr => mr.MappingRuleTypeId)
            .NotNull().WithMessage("MappingRuleTypeId is required.")
            .InclusiveBetween(1, 2).WithMessage("MappingRuleTypeId must be between 1 and 2.");            
    }
}
