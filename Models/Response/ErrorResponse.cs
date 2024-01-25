using Models.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Models.Response
{
    public class ErrorResponse
    {
        public ErrorResponse() { }

        public ErrorResponse(string fieldName, string errorMsg)
        {
            Errors.Add(new ErrorDTO() { FieldName = fieldName, Message = errorMsg });
        }
        
        public ErrorResponse(string errorMsg)
        {
            Errors.Add(new ErrorDTO() { Message = errorMsg });
        }

        public ErrorResponse(ModelStateDictionary modelState)
        {
            var errorsInModelState = modelState
                .Where(x => x.Value != null && x.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key ?? "Unknown", // Handle null key gracefully
                    kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage ?? "Unknown") ?? Enumerable.Empty<string>()
                )
                .ToArray();

            foreach (var error in errorsInModelState)
            {
                foreach (var subError in error.Value)
                {
                    var errorDto = new ErrorDTO
                    {
                        FieldName = error.Key,
                        Message = subError
                    };

                    Errors.Add(errorDto);
                }
            }
        }

        public List<ErrorDTO> Errors { get; set; } = new List<ErrorDTO>();
    }
}
