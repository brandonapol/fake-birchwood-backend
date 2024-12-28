namespace Blog;

public class BlogType
{
    public int Id { get; set;}
    public string Title { get; set; } = string.Empty;
    public virtual ICollection<BlogIngredient> Ingredients { get; set; } = new List<BlogIngredient>();
}

public class BlogIngredient
{
    public int Id { get; set; }
    public int BlogId { get; set; }
    public required virtual BlogType Blog { get; set; }
    public string Value { get; set; } = string.Empty;
}