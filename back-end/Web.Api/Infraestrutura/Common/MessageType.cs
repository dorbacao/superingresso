namespace Web.Api.Infraestrutura.Common
{
    /// <summary>
    /// Tipo da mensagem que acompanha a resposta
    /// </summary>
    public enum MessageType
    {
        Success = 1,
        Error = 2,
        Warn = 3,
        Info = 4,
        CriticalError = 5
    }
}
