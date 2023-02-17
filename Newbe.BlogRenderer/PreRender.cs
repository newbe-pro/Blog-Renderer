namespace Newbe.BlogRenderer;

internal sealed class PreRender : IPreRender
{
    private readonly IYamlFrontMatterRetriever _yamlFrontMatterRetriever;

    public PreRender(
        IYamlFrontMatterRetriever yamlFrontMatterRetriever)
    {
        _yamlFrontMatterRetriever = yamlFrontMatterRetriever;
    }

    public async Task<string> PreRenderAsync(MdRenderOptions options)
    {
        var yamlFront = await _yamlFrontMatterRetriever.GetYamlFrontAsync(options.Markdown);
        var result = options.Markdown;

        result = result.Replace(Consts.PlaceHolders.Markdown.ChatGPT, string.Empty);
        result = result.Replace(Consts.PlaceHolders.Markdown.More, string.Empty);

        // remove heading numbers like ¹²³⁴⁵
        result = result.Replace("¹", string.Empty);
        result = result.Replace("²", string.Empty);
        result = result.Replace("³", string.Empty);
        result = result.Replace("⁴", string.Empty);
        result = result.Replace("⁵", string.Empty);
        result = result.Replace("⁶", string.Empty);
        result = result.Replace("⁷", string.Empty);
        result = result.Replace("⁸", string.Empty);
        result = result.Replace("⁹", string.Empty);
        result = result.Replace("⁰", string.Empty);

        if (!string.IsNullOrWhiteSpace(yamlFront.Slag) && !string.IsNullOrWhiteSpace(options.CopyrightTemplate))
        {
            result = result.Replace(Consts.PlaceHolders.Markdown.Copyright,
                options.CopyrightTemplate.Replace(Consts.PlaceHolders.Mic.Slag, yamlFront.Slag));
        }

        if (!string.IsNullOrWhiteSpace(options.EndingTemplate))
        {
            result = result.Replace(Consts.PlaceHolders.Markdown.Ending, options.EndingTemplate);
        }

        if (!string.IsNullOrWhiteSpace(options.AdTemplate))
        {
            result = result.Replace(Consts.PlaceHolders.Markdown.Ad, options.AdTemplate);
        }

        return result;
    }
}