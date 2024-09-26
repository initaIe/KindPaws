using KindPaws.Domain.Managements.PetManagement.VOs.ValidationRules;
using KindPaws.Domain.Shared;
using KindPaws.Domain.Shared.Others;
using KindPaws.Domain.Shared.Others.Extensions;
using KindPaws.Domain.Shared.Others.Validators;
using KindPaws.Domain.Shared.VOs;
using KindPaws.Domain.Shared.VOs.Constraints;

namespace KindPaws.Domain.Managements.PetManagement.VOs;

public class PetSpecie
{
    private readonly List<PetBreed> _breeds;

    private PetSpecie(
        List<PetBreed> breeds,
        Name name,
        Description description)
    {
        _breeds = breeds;
        Name = name;
        Description = description;
    }

    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public IReadOnlyList<PetBreed> Breeds => _breeds;

    public static Result<PetSpecie, IEnumerable<string>> Create(
        List<PetBreed> breeds,
        Name name,
        Description description)
    {
        List<string> errors = [];

        if (errors.Count > 0)
            return errors;

        var petSpecie = new PetSpecie(breeds, name, description);

        return petSpecie;
    }
}