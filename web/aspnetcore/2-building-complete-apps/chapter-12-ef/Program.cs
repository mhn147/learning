using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration
    .GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite(connString));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

public class EditRecipeBase
{
    [Required, StringLength(100)]
    public required string Name { get; set; }
    [Range(0, 23), DisplayName("Time to cook (hrs)")]
    public int TimeToCookHrs { get; set; }
    [Range(0, 59), DisplayName("Time to cook (mins)")]
    public int TimeToCookMins { get; set; }
    [Required]
    public required string Method { get; set; }
    [DisplayName("Vegetarian?")]
    public bool IsVegetarian { get; set; }
    [DisplayName("Vegan?")]
    public bool IsVegan { get; set; }
}

public class CreateRecipeCommand : EditRecipeBase
{
    public IList<CreateIngredientCommand> Ingredients { get; set; } = new List<CreateIngredientCommand>();

    public Recipe ToRecipe()
    {
        return new Recipe
        {
            Name = Name,
            TimeToCook = new TimeSpan(TimeToCookHrs, TimeToCookMins, 0),
            Method = Method,
            IsVegetarian = IsVegetarian,
            IsVegan = IsVegan,
            Ingredients = Ingredients.Select(x => x.ToIngredient()).ToList()
        };
    }
}

public class CreateIngredientCommand
{
    [Required, StringLength(100)]
    public required string Name { get; set; }
    [Range(0, int.MaxValue)]
    public decimal Quantity { get; set; }
    [Required, StringLength(20)]
    public required string Unit { get; set; }

    public Ingredient ToIngredient()
    {
        return new Ingredient
        {
            Name = Name,
            Quantity = Quantity,
            Unit = Unit,
        };
    }
}

public class RecipeDetailViewModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Method { get; set; }

    public required IEnumerable<Item> Ingredients { get; set; }

    public class Item
    {
        public required string Name { get; set; }
        public required string Quantity { get; set; }
    }
}

public class RecipeSummaryViewModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string TimeToCook { get; set; }
    public int NumberOfIngredients { get; set; }

    public static RecipeSummaryViewModel FromRecipe(Recipe recipe)
    {
        return new RecipeSummaryViewModel
        {
            Id = recipe.RecipeId,
            Name = recipe.Name,
            TimeToCook = $"{recipe.TimeToCook.Hours}hrs {recipe.TimeToCook.Minutes}mins",
        };
    }
}

public class UpdateRecipeCommand : EditRecipeBase
{
    public int Id { get; set; }

    public void UpdateRecipe(Recipe recipe)
    {
        recipe.Name = Name;
        recipe.TimeToCook = new TimeSpan(TimeToCookHrs, TimeToCookMins, 0);
        recipe.Method = Method;
        recipe.IsVegetarian = IsVegetarian;
        recipe.IsVegan = IsVegan;
    }
}