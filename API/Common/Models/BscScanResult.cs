namespace Common.Models
{
    public class BscScanResult<T>
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public T? Result { get; set; }
    }
}
