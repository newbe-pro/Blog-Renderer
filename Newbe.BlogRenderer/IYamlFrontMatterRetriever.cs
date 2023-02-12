namespace Newbe.BlogRenderer;

public interface IYamlFrontMatterRetriever
{
    Task<YamlFront> GetYamlFrontAsync(string markdown);
}

public record YamlFront
{
    public string Title { get; set; } = "not found";
    public string Summary { get; set; } = "not found";
    public string Cover { get; set; } = "not found";
    public string Slag { get; set; } = "not found";
    public string Tags { get; set; } = "not found";
}