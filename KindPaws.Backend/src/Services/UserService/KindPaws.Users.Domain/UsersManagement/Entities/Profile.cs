using KindPaws.SharedKernel.DDD;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Users.Domain.UsersManagement.Entities;

// TODO: add photos + avatar
// TODO: add profile display settings
public sealed class Profile : Entity<ProfileId>
{
    private List<SocialNetwork> _socialNetworks = [];

    #region EF Core constructor

    private Profile(
        ProfileId id,
        CreatedAt createdAt)
        : base(id, createdAt)
    {
    }

    #endregion

    public Gender Gender { get; private set; } = Gender.Undefined;
    public FullName? FullName { get; private set; }
    public BirthdayAt? BirthdayAt { get; private set; }
    public ProfileDescription? Description { get; private set; }
    public Address? Address { get; private set; }
    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;

    #region Factory methods

    public static Profile CreateNew()
    {
        var id = ProfileId.CreateRandom();
        var createdAt = CreatedAt.CreateNew();

        return new Profile(
            id,
            createdAt);
    }

    public static Profile Create(
        ProfileId id,
        CreatedAt createdAt)
    {
        return new Profile(
            id,
            createdAt);
    }

    #endregion

    #region CRUD

    internal void UpdateInfo(
        Gender gender,
        FullName? fullName,
        BirthdayAt? birthdayAt,
        ProfileDescription? description,
        Address? address)
    {
        FullName = fullName;
        Gender = gender;
        BirthdayAt = birthdayAt;
        Description = description;
        Address = address;
        UpdateLastModifiedAt();
    }

    #endregion
}