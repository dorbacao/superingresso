namespace Web.Api.Infraestrutura.Common
{
    public interface IAnswer<T> : IAnswer
    {
        T Value { get; }
        bool HasValue { get; }
    }
}
