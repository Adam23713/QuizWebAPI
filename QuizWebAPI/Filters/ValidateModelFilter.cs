using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models.Response;

namespace Models.Filters
{
    public class ValidateModelFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context != null && !context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new ErrorResponse(context.ModelState));
            }
        }
    }
}
