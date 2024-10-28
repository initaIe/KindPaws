namespace KindPaws.Application.Abstractions.ExistValidators;

public interface IPetExistValidator
{
    Task<bool> IsPetByIdExists(Guid petId, CancellationToken cancellationToken);
}