using Boutiqaat.Celebrity.Core.Response;
using System.Threading.Tasks;

namespace Boutiqaat.Celebrity.Repository.Teacher
{
    public interface ITeacherRepostory
    {
        Task<ResponseMessage> AddTeacherAsync(string json);
        Task<ResponseMessage> GetTeachersAsync(string json);
        Task<ResponseMessage> EditTeacherAsync(string json);
        Task<ResponseMessage> DeleteTeacherAsync(string json);
        Task<ResponseMessage> GetTeacherByIdAsync(string json);
    }
}
