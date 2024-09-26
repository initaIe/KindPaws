namespace KindPaws.Domain.Shared.VOs;

// TODO: mb create VO Link and add here 
public class SocialNetwork
{
    private SocialNetwork(Name name, Constraints.DescriptionConstraints description)
    {
        Name = name;
        Description = description;
    }

    public Name Name { get; private set; }
    public Constraints.DescriptionConstraints Description { get; private set; }
}