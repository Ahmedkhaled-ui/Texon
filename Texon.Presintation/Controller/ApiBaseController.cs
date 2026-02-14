using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Texon.Service.Abstraction.Common;

namespace Texon.Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiBaseController : ControllerBase
    {
        protected ActionResult HandleResult(Result result)
        {
            if (result.IsSuccess)
                return NoContent();

            return HandleErrors(result.Errors);
        }

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
                return Ok(result.Value);

            return HandleErrors(result.Errors);
        }

        private ActionResult HandleErrors(IReadOnlyList<Error> errors)
        {
            if (errors == null || errors.Count == 0)
            {
                return StatusCode(
      StatusCodes.Status500InternalServerError,
      new ProblemDetails
      {
          Title = "An unexpected error occurred",
          Status = StatusCodes.Status500InternalServerError
      }
  );


            }

            if (errors.All(e => e.Type == ErrorType.Validation))
            {
                return HandleValidationProblem(errors);
            }

            return HandleSingleErrorProblem(errors[0]);
        }

        private ActionResult HandleValidationProblem(IReadOnlyList<Error> errors)
        {
            var modelState = new ModelStateDictionary();

            foreach (var error in errors)
            {
                modelState.AddModelError(
                    error.Code ?? "ValidationError",
                    error.Description
                );
            }

            return ValidationProblem(modelState);
        }

        private ActionResult HandleSingleErrorProblem(Error error)
        {
            var statusCode = error.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            return StatusCode(
                statusCode,
                new ProblemDetails
                {
                    Title = error.Description,
                    Type = error.Code,
                    Status = statusCode
                }
            );
        }
    }
}
