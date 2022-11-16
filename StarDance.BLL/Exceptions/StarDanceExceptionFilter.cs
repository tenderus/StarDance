using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StarDance.BLL.Exceptions;

public class StarDanceExceptionFilter : ExceptionFilterAttribute
{
    // public override void OnException(ExceptionContext context)
    //     {
    //         if (context.Exception is DanceClubAPIException apiException)
    //         {
    //             context.Result = new ObjectResult(apiException.Message) { StatusCode = (int)apiException.StatusCode };
    //         }
    //     }
}