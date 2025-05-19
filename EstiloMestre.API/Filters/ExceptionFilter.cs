using EstiloMestre.Communication.Responses;
using EstiloMestre.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EstiloMestre.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is EstiloMestreException)
            ProjectHandleException(context);
        else
            ThrowUnknowException(context);
    }

    private static void ProjectHandleException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ErrorOnValidationException contextException:
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(new ResponseErrorJson(contextException.ErrorsMessages));
                break;
            }
            case InvalidLoginException:
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(context.Exception.Message));
                break;
            }
        }
    }

    private static void ThrowUnknowException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesExceptions.UNKNOW_EXCEPTION));
    }
}