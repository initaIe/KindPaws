namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects.Lists;

public record PetPhotoList
{
    // ef core
    private PetPhotoList()
    {
    }

    public PetPhotoList(IEnumerable<PetPhoto> petPhotos)
    {
        Photos = petPhotos.ToList();
    }

    public IReadOnlyList<PetPhoto> Photos { get; }
}