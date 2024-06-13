namespace Web.Api.Infraestrutura.Common
{
    public interface IPaginateCommand : ICommand
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

}
