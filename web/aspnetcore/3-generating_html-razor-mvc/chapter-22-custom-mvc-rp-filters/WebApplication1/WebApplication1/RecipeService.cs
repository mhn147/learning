
namespace WebApplication1;

public class Recipe
{
    public string? Name { get; set; }
    public DateTimeOffset? LastModified { get; internal set; }
}

public class UpdateRecipeCommand
{

}

public class RecipeService
{
    public bool DoesRecipeExist(int id)
    {
        return true;
    }

    public Recipe GetRecipeDetail(int id)
    {
        return new Recipe();
    }

    internal void UpdateRecipe(UpdateRecipeCommand command)
    {
        throw new NotImplementedException();
    }
}
