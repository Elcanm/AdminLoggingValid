using AdminLoggingValid.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdminLoggingValid.Controllers
{
    public class AdminService : IAdminService
    {
        private readonly ILogger _logger;
        private AdminDbContext _adminDbContext;
        private IMapper _mapper;
        public AdminService(AdminDbContext adminDbContext, IMapper mapper, ILogger<AdminService> logger)
        {
            _adminDbContext = adminDbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<User> Add(UserDTO userDTO)
        {
            try
            {
                var data = _mapper.Map<User>(userDTO);
                _adminDbContext.Users.Add(data);
                await _adminDbContext.SaveChangesAsync();
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<User> Delete(int id)
        {
            try
            {
                User user = await _adminDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
                _adminDbContext.Users.Remove(user);
                await _adminDbContext.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        

        public async Task<List<User>> Get()
        {
            try
            {
                var user = await _adminDbContext.Users.ToListAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<User> Update(UserDTO userDTO, int id)
        {
            try
            {
                var user = _mapper.Map<User>(userDTO);
                user.Id = id;
                _adminDbContext.Users.Update(user);
                await _adminDbContext.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
