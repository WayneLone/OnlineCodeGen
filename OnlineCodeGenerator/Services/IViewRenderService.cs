using System.Threading.Tasks;

namespace OnlineCodeGenerator.Services
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model);
    }
}
