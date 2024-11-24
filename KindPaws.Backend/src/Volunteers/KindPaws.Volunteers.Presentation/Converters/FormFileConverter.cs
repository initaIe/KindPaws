using KindPaws.Core.Dtos;
using Microsoft.AspNetCore.Http;

namespace KindPaws.Volunteers.Presentation.Converters;

// TODO MOVE
public class FormFileConverter : IAsyncDisposable
{
    private readonly List<UploadFileDto> _uploadFileDtos = [];

    public async ValueTask DisposeAsync()
    {
        foreach (var uploadFileDTO in _uploadFileDtos) await uploadFileDTO.Stream.DisposeAsync();
    }

    public IReadOnlyList<UploadFileDto> Process(IFormFileCollection files)
    {
        foreach (var file in files)
        {
            var stream = file.OpenReadStream();
            var uploadFileDTO = new UploadFileDto(file.FileName, stream);
            _uploadFileDtos.Add(uploadFileDTO);
        }

        return _uploadFileDtos;
    }
}