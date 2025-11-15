using EventIngestionAPI.IntegrationEvents;
using FluentValidation;

namespace EventIngestionAPI.Validators;

public class InternalEventValidator : AbstractValidator<InternalEvent>
{
    public InternalEventValidator()
    {
        RuleFor(ie => ie.PlayerId)
            .NotEmpty().WithMessage("PlayerId is required.");

        RuleFor(ie => ie.Amount)
            .NotNull().WithMessage("Amount is required.");

        RuleFor(ie => ie.OccurredAt)
            .NotNull().WithMessage("OccurredAt is required.");
    }
}
