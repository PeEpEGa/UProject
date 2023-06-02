using TODOListUniProject.Contracts.Http;

namespace TODOList.Domain.Exceptions;

public class ListException : Exception
{
    public ErrorCode ErrorCode { get;}

    public ListException(ErrorCode errorCode) : this(errorCode, null)
    {
    }

    public ListException(ErrorCode errorCode, string message) : base(message)
    {
        ErrorCode = errorCode;
    }
}