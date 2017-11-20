using System.Threading.Tasks;

namespace PolyHxDotNetServices.Sts
{
    public interface IStsService
    {
        Task<string> GetAccessToken();
    }
}