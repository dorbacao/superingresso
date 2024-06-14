using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web.Api.Infraestrutura.Common;

namespace Web.Api.Web.Filter
{
    public class AnswerFilter : ActionFilterAttribute
    {
        public void ChangeHttpStatusWhenContainsError(ActionExecutedContext actionExecutedContext)
        {
            var result = actionExecutedContext.Result as ObjectResult;

            if (result != null)
            {
                var response = result.Value as IAnswer;

                if (response == null) return;

                //Comparar com true por utilizar o null propagation
                var hasError = response.Messages?.Any(a => a.Type == MessageType.Error) == true;
                var hasCriticalError = response.Messages?.Any(a => a.Type == MessageType.CriticalError) == true;

                if (hasError)
                {
                    result.StatusCode = response.Messages?.FirstOrDefault(a => a.Type == MessageType.Error && a.Code.HasValue)?.Code ?? (int)HttpStatusCode.BadRequest;
                }

                if (hasCriticalError)
                {
                    result.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ChangeHttpStatusWhenContainsError(context);
            base.OnActionExecuted(context);
        }

    }
}
