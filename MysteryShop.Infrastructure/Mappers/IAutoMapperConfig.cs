using AutoMapper;

namespace MysteryShop.Infrastructure.Mappers
{
    public interface IAutoMapperConfig
    {
        IMapper Mapper { get; }
    }
}