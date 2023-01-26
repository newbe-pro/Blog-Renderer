namespace Newbe.BlogRenderer;

public interface IMdTitleRetriever
{
    Task<string?> GetTitleAsync(string markdown);
}