namespace BarBreak.Core.DTOs;

// don't use this response format
// problem details - https://www.rfc-editor.org/rfc/rfc7807
// implementing problem details format - https://code-maze.com/using-the-problemdetails-class-in-asp-net-core-web-api/
// multiple errors in ErrorOr - https://github.com/amantinband/error-or?tab=readme-ov-file#support-for-multiple-errors (instead of 'return' use errors.Add();)
public class ResultDto<T>
{
    public bool Success { get; set; }

    public T Data { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
}