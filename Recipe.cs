namespace Recipe;

public class RecipeType
{
    public int Id { get; set;}
    public string Title { get; set; } = string.Empty;
    public virtual ICollection<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();
}

public class RecipeIngredient
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public required virtual RecipeType Recipe { get; set; }
    public string Value { get; set; } = string.Empty;
}