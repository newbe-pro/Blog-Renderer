namespace Newbe.BlogRenderer;

public interface IPreRender
{
    Task<string> PreRenderAsync(MdRenderOptions options);
}