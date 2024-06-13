namespace Web.Api.Infraestrutura.Common
{
    public interface IOrderByCommand : ICommand
    {
        string FieldName { get; set; }
        OrderByCommandDirection Direction { get; set; }
    }

}
