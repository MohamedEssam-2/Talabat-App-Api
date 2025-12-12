using Microsoft.AspNetCore.Mvc;
using Shared.Error_Models;

namespace TalabatDemo.Factory
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenrateApiValidationErrorResponse(ActionContext context)
        {

            var errors = context.ModelState
                    .Where(e => e.Value.Errors.Any())
                    .Select(m => new Shared.Error_Models.ValidationError()
                    {
                        Field = m.Key,
                        ErrorMessage = m.Value.Errors.Select(e => e.ErrorMessage)
                    });

            var response = new ValidationErrorToReturn()
            {
                ValidationErrors = errors
            };
            return new BadRequestObjectResult(response);



        }
    }
    
}
