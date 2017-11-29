using AutoMapper;

namespace Prsi.Mapping
{
    class MappingProfilesConfig
    {
        private static IMapper mapper;

        public static IMapper Mapper
        {
            get
            {
                return MappingProfilesConfig.mapper;
            }
        }

        public static void RegisterMapping()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Mapping.MapperProfiles.ArchTblMapperProfile>();
                cfg.AddProfile<Mapping.MapperProfiles.MessageDAOtoDTOtransformProfile>();
                cfg.AddProfile<Mapping.MapperProfiles.SmsMapperProfile>();
            });

            MappingProfilesConfig.mapper = configuration.CreateMapper();
        }
    }
}
