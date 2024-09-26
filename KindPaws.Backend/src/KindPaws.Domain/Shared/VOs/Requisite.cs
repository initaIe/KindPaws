namespace KindPaws.Domain.Shared.VOs;

public class Requisite
{
    public Requisite(
        Name name,
        Constraints.DescriptionConstraints description)
    {
        Name = name;
        Description = description;
    }

    public Name Name { get; private set; }
    public Constraints.DescriptionConstraints Description { get; private set; }
}