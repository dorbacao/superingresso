using System.Windows.Input;

namespace Web.Api.Infraestrutura.Common
{
    public class Answer : IAnswer
    {
        public Answer()
        {
            Messages = new List<Message>();
        }
        public List<Message> Messages { get; set; }

        public bool HasError
        {
            get
            {
                return Messages.Any(a => a.Type == MessageType.Error || a.Type == MessageType.CriticalError);
            }
        }
        public bool IsOk
        {
            get
            {
                return !HasError;
            }
        }

        public static Answer<T> Ok<T>(T value)
        {
            var response = new Answer<T>();
            response.Value = value;
            return response;
        }
        public static AnswerPaginate<T> Ok<T>(T value, long totalCount)
        {
            var response = new AnswerPaginate<T>();
            response.Value = value;
            response.TotalCount = totalCount;
            return response;
        }

        public static AnswerPaginate<T> Ok<T>(T value, long totalCount, string skipToken)
        {
            var response = new AnswerPaginate<T>();
            response.Value = value;
            response.TotalCount = totalCount;
            response.SkipToken = skipToken;

            return response;
        }


        public static AnswerPaginate<T> Error<T>(T value, long totalCount, string text)
        {
            var response = new AnswerPaginate<T>();
            response.Value = value;
            response.TotalCount = totalCount;
            response.AddError(text);
            return response;
        }

        public static AnswerPaginate<T> UnAuthorized<T>(long totalCount, string text)
        {
            var response = new AnswerPaginate<T>();
            response.AddUnAuthError(text);
            response.TotalCount = totalCount;
            return response;
        }
        public static Answer<T> Ok<T>()
        {
            var response = new Answer<T>();
            return response;
        }
        public static Answer<T> Ok<T>(T value, string text)
        {
            var response = new Answer<T>();
            response.Value = value;
            response.AddSuccess(text);
            return response;
        }

        public static Answer<T> Info<T>(T value, string text)
        {
            var response = new Answer<T>();
            response.Value = value;
            response.AddInfo(text);
            return response;
        }

        public static Answer<T> Info<T>(string text)
        {
            var response = new Answer<T>();
            response.AddInfo(text);
            return response;
        }

        public static Answer Ok()
        {
            var response = new Answer();
            return response;
        }

        public static async Task<Answer<T>> OkAsync<T>(T value)
        {
            var result = Ok(value);
            return await Task.FromResult(result);
        }

        public static Answer<T> Error<T>(string text)
        {
            var response = new Answer<T>();
            return response.AddError(text);
        }

        public static Answer<T> Error<T>(T value, string text)
        {
            var response = new Answer<T>();
            response.Value = value;
            response.AddError(text);
            return response;
        }

        public static Answer Error(string text)
        {
            var response = new Answer();
            return response.AddError(text);
        }

        public static Answer<T> NotFound<T>()
        {
            var response = new Answer<T>();
            return response.AddError(404, "Não encontrado");
        }

        public static Answer<T> NotFound<T>(string message)
        {
            var response = new Answer<T>();
            return response.AddError(404, message);
        }

        public static Answer NotFound(string text)
        {
            var response = new Answer();
            return response.AddError(404, text);
        }

        public static Answer NotFound()
        {
            var response = new Answer();
            return response.AddError(404, "Não encontrado");
        }

        public static Answer<T> UnAuthorized<T>(string text)
        {
            var response = new Answer<T>();
            return response.AddUnAuthError(text);
        }

        public static Answer UnAuthorized(string text)
        {
            var response = new Answer();
            return response.AddUnAuthError(text);
        }

        public static Answer<T> CriticalError<T>(string text)
        {
            var response = new Answer<T>();
            return response.AddCriticalError(text);
        }
        public static Answer<T> CriticalError<T>(string simpleMessage, Exception exception)
        {
            var response = new Answer<T>();
            return response.AddCriticalError(simpleMessage, exception.StackTrace);
        }

        public static Answer CriticalError(string simpleMessage, Exception exception)
        {
            var inner = exception?.InnerException != null ? exception?.InnerException : exception;
            var response = new Answer();
            return response.AddCriticalError(simpleMessage, inner?.Message);
        }

    }

}
