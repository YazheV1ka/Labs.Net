namespace Lab1LINQ;

public class Menu
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public double Price { get; set; }
    public ICollection<Dish>? Dishes { get; set; }
    
}