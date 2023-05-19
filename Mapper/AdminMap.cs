using AdminLoggingValid.Model;
using AutoMapper;

namespace AdminLoggingValid.Mapper
{
    public class AdminMap:Profile
    {
        public AdminMap()
        {
            CreateMap<UserDTO, User>();
        }
    }
}
