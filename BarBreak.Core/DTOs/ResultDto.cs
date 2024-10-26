namespace BarBreak.Core.DTOs
{
    public class ResultDto<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}