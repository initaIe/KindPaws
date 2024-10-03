namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects.Lists;

public record PetPhotoList
{
    // ef core
    private PetPhotoList()
    {
    }

    public PetPhotoList(List<PetPhoto> petPhotos)
    {
        Photos = petPhotos;
    }

    public IReadOnlyList<PetPhoto> Photos { get; }
}