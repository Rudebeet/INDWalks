using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace INDWalks.API.CustomActionFilter
{
	public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.ModelState.IsValid == false)
            {
                //context.Result = new BadRequestResult();
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }
    }
}

