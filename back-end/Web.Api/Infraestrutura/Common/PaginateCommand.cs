namespace Web.Api.Infraestrutura.Common
{
    public class PaginateCommand : IPaginateCommand, IOrderByCommand
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string? FieldName { get; set; }
        public OrderByCommandDirection Direction { get; set; }

        public void ThrowIfInvalid()
        {
            if (PageSize > 50)
                throw new ArgumentOutOfRangeException(nameof(PageSize));
        }
    }

}
