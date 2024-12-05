using KindPaws.Pets.Contracts.Dtos;

namespace KindPaws.Pets.Application.DataModels;

public class VolunteerDataModel
{
    public Guid Id { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset LastModifiedAt { get; init; }
    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string? Description { get; init; }
    public int? YearsOfExperience { get; init; }
    public IReadOnlyList<RequisiteDto> Requisites { get; init; } = [];
    public IReadOnlyList<PetDataModel> Pets { get; init; } = [];
}