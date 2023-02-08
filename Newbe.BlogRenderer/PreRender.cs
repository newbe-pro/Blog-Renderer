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
        result = result.Replace(Consts.PlaceHolders.Markdown.YamlFrontMatter, string.Empty);

        if (!string.IsNullOrWhiteSpace(yamlFront.Slag) && !string.IsNullOrWhiteSpace(options.CopyrightTemplate))
        {
            result = result.Replace(Consts.PlaceHolders.Markdown.Copyright,
                options.CopyrightTemplate.Replace(Consts.PlaceHolders.Mic.Slag, yamlFront.Slag));
        }

        if (!string.IsNullOrWhiteSpace(options.EndingTemplate))
        {
            result = result.Replace(Consts.PlaceHolders.Markdown.Ending, options.EndingTemplate);
        }

        return result;
    }
}