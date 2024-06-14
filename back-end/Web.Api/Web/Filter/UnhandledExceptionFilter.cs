using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Infraestrutura.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Web.Api.Web.Filter
{
    public class UnhandledExceptionFilter : IExceptionFilter
    {
        string GetFriendlyError(Exception exception)
        {
            var updateException = exception as DbUpdateException;
            if (updateException != null)
            {
                var sqlException = updateException.InnerException as SqlException;
                if (sqlException?.Number == 547)//Foreing Key Error
                {
                    return "Uma ou mais chaves estrangeiras não foi definida";
                }

                if (updateException.InnerException?.Message?.Contains("FOREIGN") == true)
                {
                    return "Uma ou mais chaves estrangeiras são inválidas";
                }
                if (updateException.InnerException?.Message?.Contains("UNIQUE") == true)
                {
                    return "Uma ou mais chaves estrangeiras não podem ser duplicadas";
                }

                if (updateException.InnerException?.Message?.ToLower().Contains("cannot insert duplicate key") == true)
                {
                    return "Não foi possível incluir dados duplicados";
                }
            }

            var formatException = exception as FormatException;
            if (formatException != null)
            {
                if (formatException.Source.Contains("System.Net.Mail"))
                {
                    return "Uma ou mais configurações de email não são válidas";
                }
            }

            return exception.Message;
        }
        public void OnException(ExceptionContext context)
        {
            var logger = context.HttpContext.RequestServices.GetService(typeof(ILogger<UnhandledExceptionFilter>)) as ILogger<UnhandledExceptionFilter>;

            logger.LogError(context.Exception, "UnhandledExceptionFilter: Ocorreu uma execepção não tratada na aplicação");

            var response = Answer.CriticalError(GetFriendlyError(context.Exception), context.Exception.InnerException);
            var result = new ObjectResult(response);
            result.StatusCode = 500;
            context.Result = result;
        }
    }
}
