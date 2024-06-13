namespace Web.Api.Infraestrutura.Common
{
    public class Answer<T> : Answer, IAnswer<T>
    {
        public T Value { get; set; }
        public bool HasValue
        {
            get
            {
                return Value != null;
            }
        }

        public Answer()
            : base()
        {

        }

        public Answer(T value)
            : base()
        {
            Value = value;
        }


        public Answer(params Message[] messages)
            : base()
        {
            Messages.AddRange(messages);
        }

        #region ' Operator '


        public static implicit operator Answer<T>(Message value)
        {
            return new Answer<T>(new Message[1] { value });
        }


        #endregion

    }
}
