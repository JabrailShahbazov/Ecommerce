using Ecommerce.Application.Common.Abstarcts;

namespace Ecommerce.Application.Common.Consecrate;

public class PaginationDto :IPagenatorDto
{
    public int Size { get; set; } = 5;
    public int Page { get; set; } = 0;
}