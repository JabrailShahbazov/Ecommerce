using Ecommerce.Application.Common.Abstarcts;

namespace Ecommerce.Application.Common.Concrates;

public interface IPagedResult<T> : IListResult<T>, IHasTotalCount
{

}