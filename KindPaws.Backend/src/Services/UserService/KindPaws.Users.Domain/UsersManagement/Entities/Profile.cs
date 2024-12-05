using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects.Ids;
using KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;

namespace KindPaws.Users.Domain.UsersManagement.Entities;

// TODO: add photos + avatar
// TODO: add profile display settings
public sealed class Profile : Entity<ProfileId>
{
    private List<SocialNetwork> _socialNetworks = [];

    // ef core
    private Profile(
        ProfileId id,
        CreatedAt createdAt)
        : base(id, createdAt)
    {
    }

    public Gender Gender { get; private set; } = Gender.Undefined;
    public FullName? FullName { get; private set; }
    public BirthdayAt? BirthdayAt { get; private set; }
    public UserDescription? Description { get; private set; }
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
        UserDescription? description,
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