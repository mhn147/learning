public class RecipeService
{
    private readonly AppDbContext _context;

    public RecipeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateRecipe(CreateRecipeCommand cmd)
    {
        var recipe = new Recipe
        {
            Name = cmd.Name,
            TimeToCook = new TimeSpan(cmd.TimeToCookHrs, cmd.TimeToCookMins, 0),
            Method = cmd.Method,
            IsVegetarian = cmd.IsVegetarian,
            IsVegan = cmd.IsVegan,
            Ingredients = cmd.Ingredients.Select(i => new Ingredient
            {
                Name = i.Name,
                Quantity = i.Quantity,
                Unit = i.Unit
            }).ToList()
        };

        _context.Add(recipe);
        await _context.SaveChangesAsync();
        return recipe.RecipeId;
    }
}