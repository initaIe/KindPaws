using System.Collections;

namespace KindPaws.SharedKernel.Others.ErrorManagement;

public class ErrorList : IEnumerable<Error>
{
    private readonly List<Error> _errors;

    public ErrorList(IEnumerable<Error> errors)
    {
        _errors = [..errors];
    }

    public void Add(Error error)
        => _errors.Add(error);

    public IEnumerator<Error> GetEnumerator()
    {
        return _errors.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static implicit operator ErrorList(List<Error> errors)
    {
        return new ErrorList(errors);
    }

    public static implicit operator ErrorList(Error error)
    {
        return new ErrorList([error]);
    }
}