namespace TODOListUniProject.Contracts.Http;

public enum ErrorCode
{
    BadRequest = 40000,
    ObjectiveNotFound = 40402,
    InternalServerError = 50000,
    DbFailureError = 50001,
}

public class ErrorResponse
{
    public ErrorCode Code { get; set; }
    public string Message { get; set; }
}