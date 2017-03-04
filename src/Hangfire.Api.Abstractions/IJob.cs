using System.Threading.Tasks;

namespace Hangfire.ApiEndpoint
{
    public interface IJob
    {
        Task Do();
    }
}
