namespace API.Services;

public class ServiceResult<T>
{
    public T Data { get; set; }
    public List<string> ErrorMessage { get; set; } = [];
    public bool Success => ErrorMessage.Count == 0;

    // Factory methods for success and failure
    public static ServiceResult<T> SuccessResult(T data) => new ServiceResult<T> { Data = data };
    public static ServiceResult<T> Failure(string message) => new ServiceResult<T>
    {
        ErrorMessage = [message]
    };

    //Testing Git on different branchez
}
