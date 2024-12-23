using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.Utilities.Validators;
using KindPaws.VolunteerRequests.Domain.ValueObjectsManagement.ValueObjectsConstraints;

namespace KindPaws.VolunteerRequests.Domain.ValueObjectsManagement.ValueObjects;

public record RejectionComment
{
    private RejectionComment(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<RejectionComment, Error> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return ErrorsGeneral.ValueIsRequired(nameof(RejectionComment));

        if (!StringValidator.IsInRange(
                input,
                RejectionCommentConstraints.MinLength,
                RejectionCommentConstraints.MaxLength))
            return ErrorsGeneral.ValueOutOfRange(nameof(RejectionComment));

        return new RejectionComment(input);
    }
}