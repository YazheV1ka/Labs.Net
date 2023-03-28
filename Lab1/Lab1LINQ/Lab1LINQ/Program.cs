namespace Lab1LINQ;

public class Program
{
    private static void Main(string[] args)
    {
        // Create categories
        var categories = new List<Category>
        {
            new() {Id = 1, Name = "Appetizers"},
            new() {Id = 2, Name = "Entrees"},
            new() {Id = 3, Name = "Desserts"}
        };

        // Create products
        var products = new List<Product>
        {
            new() {Id = 1, Name = "Chicken Breast", Calories = 250},
            new() {Id = 2, Name = "Salmon Fillet", Calories = 300},
            new() {Id = 3, Name = "Broccoli", Calories = 50},
            new() {Id = 4, Name = "Ice Cream", Calories = 200},
            new() {Id = 5, Name = "Chocolate", Calories = 500}
        };

        var products2 = new List<Product>
        {
            new() {Id = 6, Name = "Lettuce", Calories = 10},
            new() {Id = 7, Name = "Beef", Calories = 230},
            new() {Id = 8, Name = "Bread", Calories = 321},
            new() {Id = 9, Name = "Apple", Calories = 52},
            new() {Id = 10, Name = "Cheese", Calories = 474}
        };


        // Create dishes
        var dishes = new List<Dish>
        {
            new()
            {
                Id = 1,
                Name = "Grilled Chicken and Potatoes",
                Price = 15.99,
                Categories = new List<Category> {categories[1]},
                Ingredients = new List<ProductQuantity>
                {
                    new() {Id = 1,ProductId = 1, Product = products[0], Quantity = 1, DishId = 1},
                    new() {Id = 2,ProductId = 3, Product = products[2], Quantity = 1, DishId = 1}
                }
            },
            new()
            {
                Id = 2,
                Name = "Salmon with Broccoli",
                Price = 19.99,
                Categories = new List<Category> {categories[1]},
                Ingredients = new List<ProductQuantity>
                {
                    new() {Id = 3,ProductId = 2, Product = products[1], Quantity = 1, DishId = 2},
                    new() {Id = 4,ProductId = 3, Product = products[2], Quantity = 1, DishId = 2}
                }
            },
            new()
            {
                Id = 3,
                Name = "Chocolate Cake with Ice Cream",
                Price = 8.99,
                Categories = new List<Category> {categories[2]},
                Ingredients = new List<ProductQuantity>
                {
                    new() {Id = 5, ProductId = 4, Product = products[3], Quantity = 1, DishId = 3},
                    new() {Id = 6, ProductId = 5, Product = products[4], Quantity = 2, DishId = 3}
                }
            }
        };
        var dishes2 = new List<Dish>
        {
            new()
            {
                Id = 4,
                Name = "Salad",
                Price = 8.99,
                Categories = new List<Category> {categories[1]},
                Ingredients = new List<ProductQuantity>
                {
                    new() {Id = 7, ProductId = 2, Product = products[1], Quantity = 1, DishId = 4},
                    new() {Id = 8, ProductId = 3, Product = products[2], Quantity = 1, DishId = 4}
                }
            }
        };

        // Create menu
        var menus = new List<Menu>
        {
            new()
            {
                Id = 1, Date = DateTime.Today, Price = 25.99, Dishes = new List<Dish> {dishes[0], dishes[1], dishes[2]}
            },
            new()
            {
                Id = 2, Date = DateTime.Parse("27.03.2023 0:00:00"), Price = 30.29,
                Dishes = new List<Dish> {dishes[0], dishes[1], dishes[2]}
            }
        };

        // Linq Query
        LinqQuery(dishes, dishes2, categories, products, products2, menus);
    }


    private static void LinqQuery(List<Dish> dishes, List<Dish> dishes2, List<Category> categories, List<Product> products, List<Product> products2, List<Menu> menus)
    {
        // Query 1: Get all the dishes that contain chicken breast as an ingredient
        ChangeColorForQueryTitle("1. Get all the dishes that contain chicken breast as an ingredient");

        var chickenDishes = dishes.Where(dish =>
            dish.Ingredients!.Any(ingredient => ingredient.Product!.Name == "Chicken Breast"));
        Console.WriteLine("Dishes containing chicken breast:");
        foreach (var dish in chickenDishes) Console.WriteLine(dish.Name);
        Console.WriteLine();

        // Query 2: Get all the desserts
        ChangeColorForQueryTitle("2. Get all the desserts");
        var desserts = dishes.Where(dish => dish.Categories!.Any(category => category.Name == "Desserts"));
        Console.WriteLine("Desserts:");
        foreach (var dish in desserts) Console.WriteLine(dish.Name);


        // Query 3: Get the total number of calories in a menu
        ChangeColorForQueryTitle("3. Get the total number of calories in a menu");
        var totalCalories = menus.Sum(menu => menu.Dishes!.Sum(dish =>
            dish.Ingredients!.Sum(ingredient => ingredient.Quantity * ingredient.Product!.Calories)));
        Console.WriteLine($"Total calories in the menu: {totalCalories}");


        // Query 4: Get the average price of a dish
        ChangeColorForQueryTitle("4: Get the average price of a dish");
        var averageDishPrice = dishes.Average(dish => dish.Price);
        Console.WriteLine($"Average dish price: {averageDishPrice}");


        // Query 5: Get the most expensive dish
        ChangeColorForQueryTitle("5: Get the most expensive dish");
        var mostExpensiveDish = dishes.MaxBy(dish => dish.Price);
        Console.WriteLine($"Most expensive dish: {mostExpensiveDish!.Name}");
        Console.WriteLine();

        // Query 6: Get the cheapest dessert
        ChangeColorForQueryTitle("6: Get the cheapest dessert");
        var cheapestDessert = desserts.MinBy(dish => dish.Price);
        Console.WriteLine($"Cheapest dessert: {cheapestDessert!.Name}");
        Console.WriteLine();

        // Query 7: Get all products where calories > 200
        ChangeColorForQueryTitle("7: Get all products where calories > 200");
        var highCalorieProducts = products.Where(product => product.Calories > 200).ToList();
        Console.WriteLine("Products where calories > 200:");
        foreach (var product in highCalorieProducts)
            Console.WriteLine($"{product.Name} has {product.Calories} calories.");


        // Query 8: Get all categories with broccoli 
        ChangeColorForQueryTitle("8: Get all categories with broccoli");
        var broccoliCategories = dishes.SelectMany(d => d.Categories!).Distinct()
            .Where(c => dishes.Any(d =>
                d.Categories!.Contains(c) && d.Ingredients!.Any(pq => pq.Product!.Name!.Contains("Broccoli"))));
        Console.WriteLine("All dishes with broccoli:");
        foreach (var category in broccoliCategories) Console.WriteLine(category.Name);


        // Query 9: Get all dishes with no chocolate 
        ChangeColorForQueryTitle("9: Get all dishes with no chocolate");
        var noChocolateDishes = dishes.Where(d => d.Ingredients!.All(pq => pq.Product!.Name != "Chocolate"));
        Console.WriteLine("All dishes with no chocolate:");
        foreach (var dish in noChocolateDishes) Console.WriteLine(dish.Name);


        // Query 10: Get number of dishes
        ChangeColorForQueryTitle("10: Get number of dishes");
        Console.WriteLine($"Number of dishes: {dishes.Count}");


        // Query 11: Left join between Dish and Category on Dish.Id and Category.Id
        ChangeColorForQueryTitle("11: Left join between Dish and Category on Dish.Id and Category.Id");
        var leftJoinResult = dishes.GroupJoin(categories,
            d => d.Id,
            c => c.Id,
            (d, c) => new {DishName = d.Name, Categories = c.DefaultIfEmpty().Select(x => x.Name)});
        Console.WriteLine("Left join result:");
        foreach (var dish in leftJoinResult)
        {
            foreach (var category in dish.Categories)
            {
                Console.WriteLine($"Dish names:{dish.DishName}, Dish categories:{category}");
            }
        }


        // Query 12: Order by Price in Dishes
        ChangeColorForQueryTitle("12: Order by Price in Dishes");
        var orderByResult = dishes.OrderBy(d => d.Price);
        foreach (var dish in orderByResult) Console.WriteLine($"Dish name: {dish.Name}, Dish price: {dish.Price}");


        // Query 13: Group by Date in Menu
        ChangeColorForQueryTitle("13: Group by Date in Menu");
        var groupByResult = menus.GroupBy(m => m.Date)
            .Select(g => new {Date = g.Key, Count = g.Count()});
        foreach (var dish in groupByResult) Console.WriteLine($"Dish date: {dish.Date}");


        // Query 14: Concatenate two lists of dishes
        ChangeColorForQueryTitle("14: Concatenate two lists of dishes");
        var allDishes = dishes.Concat(dishes2);
        foreach (var dish in allDishes) Console.WriteLine(dish.Name);


        // Query 15: Concatenate two lists of products
        ChangeColorForQueryTitle("15: Concatenate two lists of products");
        var allProducts = products.Concat(products2);
        foreach (var product in allProducts) Console.WriteLine(product.Name);


        // Query 16: Union dish and product
        ChangeColorForQueryTitle("16: Union dish and product");
        var union = (from dish in dishes
                select dish.Name)
            .Union(from product in products
                select product.Name);
        foreach (var str in union) Console.WriteLine(str);


        // Query 17: Inner join: Get all dishes and their categories:
        ChangeColorForQueryTitle(
            "17: Inner join: Get all dishes and their categories:");
        var innerJoin = dishes.GroupJoin(
            categories,
            dish => dish.Categories!.FirstOrDefault()?.Id,
            category => category.Id,
            (dish, categories) => new {Dish = dish, Categories = categories}
        ).SelectMany(
            x => x.Categories.DefaultIfEmpty(),
            (x, category) => new {x.Dish, Category = category}
        );
        foreach (var str in innerJoin) Console.WriteLine($"Dish names:{str.Dish.Name}, Categories:{str.Category.Name}");


        // Query 18: Get all dishes with their names and product identifiers that belong to the "Entrees" category:
        ChangeColorForQueryTitle("18: Get all dishes with their names and product identifiers that belong to the 'Entrees' category");
        var result = dishes.Where(dish => dish.Categories!.Any(category => category.Name == "Entrees"))
            .Select(dish => new
            {
                DishId = dish.Id,
                DishName = dish.Name,
                Products = dish.Ingredients!.Select(ingredient => new
                {
                    ProductId = ingredient.Product!.Id,
                    ProductName = ingredient.Product.Name
                })
            });
        foreach (var str in result)
        {
            foreach (var product in str.Products)
            {
                Console.WriteLine($"Dish Id:{str.DishId},Dish names:{str.DishName}, Products:{product}");
            }
        }
 

        // Query 19: Get all dishes with name contains "with":
        ChangeColorForQueryTitle("19: Get all dishes with name contains 'with'");
        const string search = "with";
        var dishesContainsWith = dishes.Where(d => d.Name!.Contains(search));
        foreach (var dish in dishesContainsWith) Console.WriteLine($"Dish name: {dish.Name}");
       

        
        // Query 20: Get all possible combinations of categories and products:
        ChangeColorForQueryTitle("20: Get all possible combinations of categories and products");
        var categoryProductCombinations = from category in categories
            from product in products
            select new {Category = category, Product = product};

        foreach (var str in categoryProductCombinations)
            Console.WriteLine($"Product names:{str.Product.Name}, Category names:{str.Category.Name}");
    }


    private static void ChangeColorForQueryTitle(string queryName)
    {
        // встановлюємо колір
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(queryName);
        Console.ResetColor();
    }
}