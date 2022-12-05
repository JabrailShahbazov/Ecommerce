namespace Ecommerce.Application.ViewModels.Product;

public class ProductListDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
    public DateTime CreateDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}