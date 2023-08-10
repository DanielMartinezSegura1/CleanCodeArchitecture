namespace Shared;

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public Dictionary<string, string> ValidationErrors { get; set; }
    public List<string> Errors { get; set; }
    public List<string> Messages { get; set; }
    public T Value { get; set; }

    public static Result<T> Success(T value = default, Dictionary<string, string> validationErrors = default,
        List<string> errors = default,
        List<string> messages = default)
    {
        return new Result<T>
        {
            IsSuccess = true,
            ValidationErrors = validationErrors,
            Errors = errors,
            Messages = messages,
            Value = value
        };
    }

    public static Result<T> Fail(Dictionary<string, string> validationErrors = default, List<string> errors = default,
        List<string> messages = default)
    {
        return new Result<T>
        {
            IsSuccess = false,
            ValidationErrors = validationErrors,
            Errors = errors,
            Messages = messages,
            Value = default
        };
    }
}