using AutoMapper;
using MakeMeRich.Application.Common.Mappings;

namespace MakeMeRich.Application.UnitTests.Common
{
    public sealed class DtoResponseHandlerTestFixture
    {
        public DtoResponseHandlerTestFixture()
        {
            var configurationProvider = new MapperConfiguration(config =>
                config.AddProfile<MappingProfile>());

            Mapper = configurationProvider.CreateMapper();
        }

        public IMapper Mapper { get;}
    }
}
