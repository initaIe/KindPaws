using KindPaws.Application.DTOs;

namespace KindPaws.API.Processors;

public class FormFileProcessor : IAsyncDisposable
{
    private readonly List<UploadFileDTO> _uploadFileDtos = [];

    public async ValueTask DisposeAsync()
    {
        foreach (var uploadFileDTO in _uploadFileDtos) await uploadFileDTO.Stream.DisposeAsync();
    }

    public List<UploadFileDTO> Process(IFormFileCollection files)
    {
        foreach (var file in files)
        {
            var stream = file.OpenReadStream();
            var uploadFileDTO = new UploadFileDTO(file.FileName, stream);
            _uploadFileDtos.Add(uploadFileDTO);
        }

        return _uploadFileDtos;
    }
}