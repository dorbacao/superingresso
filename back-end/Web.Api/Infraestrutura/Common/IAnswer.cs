namespace Web.Api.Infraestrutura.Common
{
    /// <summary>
    /// Contrato Simples para Api mista(privada e pública). este contrato tem por objetivo definir a resposta de todo o serviço implementado nas controllers
    /// </summary>
    public interface IAnswer
    {
        /// <summary>
        /// Lista de Mensagens que podem acompanhar a resposta.
        /// </summary>
        List<Message> Messages { get; }

        /// <summary>
        /// Propriedade que indica quando a resposta possui mensagens de erro
        /// </summary>
        bool HasError { get; }

        /// <summary>
        /// Propriedade que indica quando não há erros na resposta
        /// </summary>
        bool IsOk { get; }
    }
}
