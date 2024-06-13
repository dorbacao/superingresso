namespace Web.Api.Infraestrutura.Common
{
    /// <summary>
    /// Métodos de extensão para a interface e todos os tipos derivados da interface IResponse
    /// </summary>
    public static class AnswerExtensions
    {

        public static Answer<T> Cast<T>(this IAnswer response)
        {
            var answer = new Answer<T>(response.Messages.ToArray());
            return answer;
        }

        /// <summary>
        /// Método que adiciona uma mensagem a lista de mensagens
        /// </summary>
        /// <typeparam name="TResponse">Retorno genérico</typeparam>
        /// <param name="response"></param>
        /// <param name="type">Tipo da mensagem de erro</param>
        /// <param name="text">Texto da mensagem associada a resposta</param>
        /// <param name="code">Código numérico de identificação do erro</param>
        /// <returns></returns>
        public static TResponse AddMessage<TResponse>(this TResponse response, MessageType type, int code, string[] text)
            where TResponse : IAnswer
        {

            text.ToList().ForEach(msg => response.Messages.Add(Message.Create(msg, code, type)));
            return response;
        }

        public static TResponse AddMessage<TResponse>(this TResponse response, MessageType type, int code, string msg, string detail)
            where TResponse : IAnswer
        {

            response.Messages.Add(Message.Create(msg, code, type, detail));
            return response;
        }

        /// <summary>
        /// Método que adiciona uma mensagem a lista de mensagens
        /// </summary>
        /// <typeparam name="TResponse">Retorno genérico</typeparam>
        /// <param name="response"></param>
        /// <param name="type">Tipo da mensagem de erro</param>
        /// <param name="text">Texto da mensagem associada a resposta</param>
        /// <returns></returns>
        public static TResponse AddMessage<TResponse>(this TResponse response, MessageType type, string[] text)
            where TResponse : IAnswer
        {

            text.ToList().ForEach(msg => response.Messages.Add(Message.Create(msg, type)));
            return response;
        }

        /// <summary>
        /// Método que adiciona uma mensagem especificamente do tipo erro a uma lista de mensagem
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="response"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static TResponse AddError<TResponse>(this TResponse response, params string[] text)
            where TResponse : IAnswer
        {
            return response.AddMessage(MessageType.Error, text);
        }

        /// <summary>
        /// Método que adiciona uma mensagem especificamente do tipo erro a uma lista de mensagem
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="response"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static TResponse AddError<TResponse>(this TResponse response, int code, params string[] text)
            where TResponse : IAnswer
        {
            return response.AddMessage(MessageType.Error, code, text);
        }


        public static TResponse AddUnAuthError<TResponse>(this TResponse response, params string[] text)
            where TResponse : IAnswer
        {
            return response.AddMessage(MessageType.Error, 401, text);
        }

        public static TResponse AddNotFoundError<TResponse>(this TResponse response, params string[] text)
            where TResponse : IAnswer
        {
            return response.AddMessage(MessageType.Error, 404, text);
        }

        /// <summary>
        /// Método que adiciona uma mensagem indicando um erro Crítico, estes erros costumeiramente estão relacionados a problemas técnicos não foram previsto pela aplicaçãom
        /// tal como permissionamento de disco, acesso a internet, etc.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="response"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static TResponse AddCriticalError<TResponse>(this TResponse response, params string[] text)
            where TResponse : IAnswer
        {
            return response.AddMessage(MessageType.CriticalError, text);
        }

        public static TResponse AddCriticalError<TResponse>(this TResponse response, string text, string detail)
            where TResponse : IAnswer
        {
            return response.AddMessage(MessageType.CriticalError, 500, text, detail);
        }

        /// <summary>
        /// Método que adiciona uma lista de mensagem especificamento do tipo 'Alerta' a uma lista de mensagens
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="response"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static TResponse AddWarn<TResponse>(this TResponse response, params string[] text)
            where TResponse : IAnswer
        {
            return response.AddMessage(MessageType.Warn, text);

        }

        /// <summary>
        /// Método que adiciona uma lista de mensagem especificamento do tipo 'Alerta' a uma lista de mensagens
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="response"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static TResponse AddInfo<TResponse>(this TResponse response, params string[] text)
          where TResponse : IAnswer
        {
            return response.AddMessage(MessageType.Info, text);


        }

        /// <summary>
        /// Método que adiciona uma lista de mensagem especificamento do tipo 'Alerta' a uma lista de mensagens
        /// </summary>
        /// <typeparam name="TResponse">Qualquer response que herde de IResponse</typeparam>
        /// <param name="response"></param>
        /// <param name="text">Lista de mensagens a serem adicionadas a resposta</param>
        /// <returns></returns>
        public static TResponse AddSuccess<TResponse>(this TResponse response, params string[] text)
            where TResponse : IAnswer
        {
            return response.AddMessage(MessageType.Success, text);

        }


    }

}
