using KindPaws.Application.Volunteers.DTOs;

namespace KindPaws.API.Processors;

public class FormFileProcessor : IAsyncDisposable
{
    private readonly List<FileDTO> _fileDtos = [];

    public async ValueTask DisposeAsync()
    {
        foreach (var fileDto in _fileDtos) await fileDto.Stream.DisposeAsync();
    }

    public List<FileDTO> Process(IFormFileCollection files)
    {
        foreach (var file in files)
        {
            var stream = file.OpenReadStream();
            var fileDto = new FileDTO(file.FileName, stream);
            _fileDtos.Add(fileDto);
        }

        return _fileDtos;
    }
}