using AutoMapper;
using url_shorten_api.DTO;
using url_shorten_api.Models;

namespace url_shorten_api.Helpers
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<URLShort, URLForReturnDTO>(); // means you want to map from URLShort to URLForReturnDTO
        }
    }
}