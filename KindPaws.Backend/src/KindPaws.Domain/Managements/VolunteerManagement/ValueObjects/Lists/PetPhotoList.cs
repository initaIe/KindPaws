namespace KindPaws.Domain.Managements.VolunteerManagement.ValueObjects.Lists;

public record PetPhotoList
{
    private readonly List<PetPhoto> _petPhotos;

    public PetPhotoList()
    {
    }

    public PetPhotoList(List<PetPhoto> petPhotos)
    {
        _petPhotos = petPhotos;
    }

    public IReadOnlyList<PetPhoto> Photos => _petPhotos;
}