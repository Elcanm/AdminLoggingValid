using AdminLoggingValid.Model;

namespace AdminLoggingValid.Controllers
{
    public interface IAdminService
    {
        public Task<List<User>> Get();
        public Task<User> Add(UserDTO userDTO);
        public Task<User> Update(UserDTO userDTO, int id);
        public Task<User> Delete(int id);
    }
}
