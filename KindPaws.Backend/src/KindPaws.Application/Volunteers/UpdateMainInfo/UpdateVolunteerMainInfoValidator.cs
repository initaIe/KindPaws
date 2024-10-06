using FluentValidation;
using KindPaws.Application.Validation;
using KindPaws.Application.Volunteers.UpdateMainInfo.DTOs;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects;
using KindPaws.Domain.Shared.ValueObjects.BaseValueObjects;
using KindPaws.Domain.Shared.ValueObjects.IDs;

namespace KindPaws.Application.Volunteers.UpdateMainInfo;

public class UpdateVolunteerMainInfoValidator : AbstractValidator<UpdateVolunteerMainInfoRequest>
{
    public UpdateVolunteerMainInfoValidator()
    {
        
    }
}