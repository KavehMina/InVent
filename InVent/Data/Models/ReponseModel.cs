namespace InVent.Data.Models
{
    public class ResponseModel<T>
    {
        public required bool Success { get; set; }
        public List<T>? Entities { get; set; }
        public required string Message { get; set; }
    }
}
