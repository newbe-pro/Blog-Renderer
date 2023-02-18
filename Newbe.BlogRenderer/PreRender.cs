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

        var indexOf = result.IndexOf(Consts.PlaceHolders.Markdown.Ai, StringComparison.Ordinal);
        if (indexOf > 0)
        {
            result = $"{result[..indexOf]}{options.AiTemplate}";
        }

        if (!string.IsNullOrWhiteSpace(yamlFront.Slag) && !string.IsNullOrWhiteSpace(options.CopyrightTemplate))
        {
            result = result.Replace(Consts.PlaceHolders.Markdown.Copyright,
                options.CopyrightTemplate.Replace(Consts.PlaceHolders.Mic.Slag, yamlFront.Slag));
        }

        if (!string.IsNullOrWhiteSpace(options.EndingTemplate))
        {
            var endingTemplate =
                MultiplePlatformTemplate.TryParse(options.EndingTemplate, null, out var endingTemplates)
                    ? endingTemplates[options.Platform]
                    : options.EndingTemplate;

            result = result.Replace(Consts.PlaceHolders.Markdown.Ending, endingTemplate);
        }

        if (!string.IsNullOrWhiteSpace(options.AdTemplate))
        {
            var adTemplate = MultiplePlatformTemplate.TryParse(options.AdTemplate, null, out var adTemplates)
                ? adTemplates[options.Platform]
                : options.AdTemplate;

            result = result.Replace(Consts.PlaceHolders.Markdown.Ad, adTemplate);
        }

        return result;
    }
}

internal record MultiplePlatformTemplate : IParsable<MultiplePlatformTemplate>
{
    public MultiplePlatformTemplate(string defaultTemplate, Dictionary<RenderPlatform, string> templates)
    {
        DefaultTemplate = defaultTemplate;
        _templates = templates;
    }

    private readonly Dictionary<RenderPlatform, string> _templates = new();
    public string DefaultTemplate { get; }
    public IReadOnlyDictionary<RenderPlatform, string> PlatformTemplates => _templates;

    public string this[RenderPlatform index]
    {
        get
        {
            if (_templates.TryGetValue(index, out var result))
            {
                return result;
            }

            return DefaultTemplate;
        }
    }

    public static MultiplePlatformTemplate Parse(string s, IFormatProvider? provider)
    {
        if (TryParse(s, provider, out var result))
        {
            return result;
        }

        throw new FormatException("Failed to parse multiple platform template.");
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out MultiplePlatformTemplate result)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            result = default!;
            return false;
        }

        var dic = new Dictionary<RenderPlatform, string>();
        // try to collect platform specific templates
        // &&&&---default---
        // default template
        // something here
        // &&&&---Wechat---
        // Wechat template
        // something here
        // &&&&---Cnblogs---
        // Cnblogs template
        // something here
        var parts = s.Split("&&&&");
        var defaultTemplate = string.Empty;
        foreach (var part in parts.Where(x => !string.IsNullOrWhiteSpace(x)))
        {
            // get platform
            var rest = part.TrimStart('&', '-');
            if (rest.StartsWith("default", StringComparison.OrdinalIgnoreCase))
            {
                defaultTemplate = rest["default---".Length..];
            }
            else
            {
                var firstIndex = rest.IndexOf("---", StringComparison.Ordinal);
                if (firstIndex > 0)
                {
                    var platformStr = rest[..firstIndex];
                    if (Enum.TryParse<RenderPlatform>(platformStr, true, out var p))
                    {
                        dic[p] = rest[(firstIndex + 3)..];
                    }
                    else
                    {
                        throw new FormatException("Failed to parse multiple platform template.");
                    }
                }
                else
                {
                    throw new FormatException("Failed to parse multiple platform template.");
                }
            }
        }

        result = new MultiplePlatformTemplate(defaultTemplate, dic);
        return true;
    }
}