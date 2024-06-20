namespace DotNet8.MiniBankingManagementSystem.Models.Features;

public class Result<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public static Result<T> SuccessResult(T data, string message = "Successful.")
    {
        return new Result<T> { Data = data, Message = message, Success = true };
    }

    public static Result<T> SuccessResult(string message = "Successful.")
    {
        return new Result<T> { Success = true, Message = message };
    }

    public static Result<T> FailureResult(string message = "Fail.")
    {
        return new Result<T> { Success = false, Message = message };
    }

    public static Result<T> FailureResult(Exception ex)
    {
        return new Result<T> { Success = false, Message = ex.ToString() };
    }

    public static Result<T> ExecuteResult(int result)
    {
        return result > 0 ? Result<T>.SuccessResult() : Result<T>.FailureResult();
    }
}
