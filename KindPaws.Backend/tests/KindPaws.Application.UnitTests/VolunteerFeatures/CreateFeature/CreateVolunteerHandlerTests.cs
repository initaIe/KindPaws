using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using KindPaws.Application.Abstractions;
using KindPaws.Application.DTOs;
using KindPaws.Application.Volunteers.VolunteersHandlers.Create;
using KindPaws.Domain.Managements.VolunteersManagement.ValueObjects;
using KindPaws.Domain.Shared.Others;
using KindPaws.UnitTests.Shared;
using Microsoft.Extensions.Logging;
using Moq;

namespace KindPaws.Application.UnitTests.VolunteerFeatures.CreateFeature;

public class CreateVolunteerHandlerTests
{
    [Fact]
    public async void Handle_ShouldReturnSuccessResult()
    {
        // ARRANGE
        var volunteer = Helpers.CreateVolunteer();
        var cancellationToken = new CancellationTokenSource().Token;

        var fullName = new FullNameDTO(
            "Test",
            "Test",
            "Test");
        var command = new CreateVolunteerCommand(fullName, "test@test.test", "89519533803");

        // repository mock
        var volunteersRepositoryMock = new Mock<IVolunteersRepository>();
        volunteersRepositoryMock
            .Setup(v => v.AddAsync(volunteer, cancellationToken))
            .Returns(Task.CompletedTask);

        var emailAddress = EmailAddress.Create(command.EmailAddress).Value;
        volunteersRepositoryMock
            .Setup(v => v.GetByEmailAddressAsync(emailAddress, cancellationToken))
            .ReturnsAsync(Errors.General.RecordAlreadyExist());

        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
        volunteersRepositoryMock
            .Setup(v => v.GetByPhoneNumberAsync(phoneNumber, cancellationToken))
            .ReturnsAsync(Errors.General.RecordAlreadyExist());

        // logger mock
        var loggerMock = new Mock<ILogger<CreateVolunteerHandler>>();

        // validator mock
        var validatorMock = new Mock<IValidator<CreateVolunteerCommand>>();
        validatorMock.Setup(v => v.ValidateAsync(command, cancellationToken))
            .ReturnsAsync(new ValidationResult());

        // unitOfWorkMock
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(cancellationToken))
            .Returns(Task.CompletedTask);

        var handler = new CreateVolunteerHandler(
            volunteersRepositoryMock.Object,
            loggerMock.Object,
            validatorMock.Object,
            unitOfWorkMock.Object);

        // ACT
        var result = await handler.HandleAsync(command, cancellationToken);

        // ASSERT
        result.IsSuccess
            .Should()
            .BeTrue();
    }
}