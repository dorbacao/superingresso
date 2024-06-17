using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Web.Api.Infraestrutura.Common
{

    public static class QueryableExtensions
    {

        public static IQueryable<TEntity> Paginate<TEntity>(this IQueryable<TEntity> repository, IPaginateCommand paginateCommand)
            where TEntity : class
        {
            var index = paginateCommand.PageIndex;
            var size = paginateCommand.PageSize;
            if (index == 0)
            {
                throw new ArgumentOutOfRangeException("PageIndex");
            }
            if (size == 0)
            {
                throw new ArgumentOutOfRangeException("PageSize");
            }

            return repository.Skip((index - 1) * size).Take(size);
        }

        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> queryToOrderBy, IOrderByCommand orderCommand, string defaultField)
            where TEntity : class
        {
            if (!string.IsNullOrWhiteSpace(orderCommand.FieldName))
            {
                if (orderCommand.Direction == OrderByCommandDirection.Ascending)
                {
                    return queryToOrderBy.OrderBy(a => EF.Property<object>(a, orderCommand.FieldName));
                }
                else if (orderCommand.Direction == OrderByCommandDirection.Descending)
                {
                    return queryToOrderBy.OrderByDescending(a => EF.Property<object>(a, orderCommand.FieldName));
                }
                else if (orderCommand.Direction == OrderByCommandDirection.Undefined)
                {
                    //Sort default field ascending direction
                    return queryToOrderBy.OrderBy(a => EF.Property<object>(a, defaultField));
                }
            }

            return queryToOrderBy;
        }


        public static IEnumerable<TEntity> OrderBy<TEntity>(this IEnumerable<TEntity> queryToOrderBy, IOrderByCommand orderCommand)
            where TEntity : class
        {
            PropertyInfo prop = null;

            if (!string.IsNullOrWhiteSpace(orderCommand.FieldName))
            {
                prop = typeof(TEntity).GetProperty(orderCommand.FieldName);

                if (prop == null)
                {
                    throw new Exception($"Propriedade {orderCommand.FieldName} não encontrada");
                }

                if (orderCommand.Direction == OrderByCommandDirection.Ascending)
                {
                    return queryToOrderBy.OrderBy(a => prop.GetValue(a, null));
                }

                return queryToOrderBy.OrderByDescending(a => prop.GetValue(a, null));
            }

            return queryToOrderBy;
        }

    }

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
