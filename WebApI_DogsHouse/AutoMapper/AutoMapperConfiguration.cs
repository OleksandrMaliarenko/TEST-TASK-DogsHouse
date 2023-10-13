using AutoMapper;
using BLL_DogsHouse.AutoMapper;

namespace WebApI_DogsHouse.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static IMapper AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return mapper;
        }
    }
}
