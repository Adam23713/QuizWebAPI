namespace Models.Models.DTO
{
    /// <summary>
    /// ErrorDTO for error responses. Gives an error description.
    /// </summary>
    public class ErrorDTO
    {
        public string? FieldName { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
