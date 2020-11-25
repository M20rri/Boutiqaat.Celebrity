using Boutiqaat.Celebrity.Core.Response;
using System.Threading.Tasks;

namespace Boutiqaat.Celebrity.Repository.Teacher
{
    public interface IAuthRepository
    {
        Task<AuthorizeResponse> Authenticate(string email, string password);

    }
}
