namespace KindPaws.Domain.Managements.VolunteersManagement.ValueObjects.Lists;

public record PetPhotoList
{
    private readonly List<PetPhoto> _petPhotos;

    private PetPhotoList()
    {
    }

    public PetPhotoList(List<PetPhoto> petPhotos)
    {
        _petPhotos = petPhotos;
    }

    public IReadOnlyList<PetPhoto> Photos => _petPhotos;
}