using EventIngestionAPI.IntegrationEvents;
using FluentValidation;

namespace EventIngestionAPI.Validators;

public class InternalEventValidator : AbstractValidator<InternalEvent>
{
    public InternalEventValidator()
    {
        RuleFor(ie => ie.PlayerId)
            .NotEmpty().WithMessage("PlayerId - Mapping rule was not found or field is empty. PlayerId is required.");

        RuleFor(ie => ie.Amount)
            .NotNull().WithMessage("Amount - Mapping rule was not found or field is empty. Amount is required.");

        RuleFor(ie => ie.OccurredAt)
            .NotNull().WithMessage("OccurredAt - Mapping rule was not found or field is empty. OccurredAt is required.");
    }
}
