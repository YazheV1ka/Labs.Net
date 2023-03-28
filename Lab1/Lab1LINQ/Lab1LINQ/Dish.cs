namespace Lab1LINQ;

public class Dish
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public ICollection<Category>? Categories { get; set; }
    public ICollection<ProductQuantity>? Ingredients { get; set; }
   
}