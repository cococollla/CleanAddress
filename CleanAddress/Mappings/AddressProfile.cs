using AutoMapper;
using CleanAddress.Dadata.Client;
using CleanAddress.Models;

namespace CleanAddress.Mappings
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<CleanAddressDto, Address>()
                .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.result))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.country))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.postal_code));

        }
    }
}
