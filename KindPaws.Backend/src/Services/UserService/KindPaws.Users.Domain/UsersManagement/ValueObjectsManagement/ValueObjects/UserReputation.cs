namespace KindPaws.Users.Domain.UsersManagement.ValueObjectsManagement.ValueObjects;

public record UserReputation
{
    public static readonly UserReputation Default = new(DefaultReputationValue);
    private const int DefaultReputationValue = 0;
    private const int UnitOfChange = 1;

    private UserReputation(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static UserReputation CreateNew()
        => new UserReputation(DefaultReputationValue);

    public static UserReputation Create(int input)
        => new UserReputation(input);

    public UserReputation GetIncreased()
        => Create(Value + UnitOfChange);

    public UserReputation GetDecreased()
        => Create(Value - UnitOfChange);
}