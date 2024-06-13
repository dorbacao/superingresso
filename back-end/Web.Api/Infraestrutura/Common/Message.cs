namespace Web.Api.Infraestrutura.Common
{
    /// <summary>
    /// Classe que representa a mensagem que deve acompanhar a resposta
    /// </summary>
    public sealed class Message
    {
        #region [ Constructores ]
        internal Message()
        {

        }
        public Message(int code, string text, MessageType type)
        {
            Code = code;
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Type = type;
        }

        public Message(int code, string text, string detail, MessageType type)
        {
            Code = code;
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Detail = detail ?? throw new ArgumentNullException(nameof(detail));
            Type = type;
        }

        #endregion

        #region [ Public Properties ]

        /// <summary>
        /// Propriedade que identifica o erro por meio de um código único.
        /// </summary>
        public int? Code { get; set; }

        /// <summary>
        /// Texto amigável da mensagem
        /// </summary>
        public string Text { get; set; }
        public string Detail { get; set; }

        /// <summary>
        /// Tipo da mensagem, utilizada para identificar a natureza da mensagem
        /// </summary>
        public MessageType Type { get; set; }

        #endregion

        #region [ Public Methods ]
        public static Message Create(string messageText, MessageType type)
        {
            return new Message() { Text = messageText, Type = type };
        }

        public static Message Create(string messageText, int code, MessageType type)
        {
            return new Message() { Text = messageText, Type = type, Code = code };
        }

        public static Message Create(string messageText, int code, MessageType type, string detail)
        {
            return new Message() { Text = messageText, Type = type, Code = code, Detail = detail };
        }

        #endregion

    }
}
