using Application.Command.Consult;
using Application.Command.Customer;
using AutoMapper;
using Domain.DTO.Consult;
using Domain.DTO.Customer;

namespace Api.Extensions
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CustomerCommand, CustomerDTO>()
                                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                                    .ForMember(dest => dest.Document, opt => opt.MapFrom(src => src.Document))
                                    .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.Birthdate))
                                    .ForPath(dest => dest.Adress, opt => opt.MapFrom(src => src.Adress))
                                    .ForPath(dest => dest.NumberAdress, opt => opt.MapFrom(src => src.NumberAdress))
                                    .ForPath(dest => dest.CEP, opt => opt.MapFrom(src => src.CEP))
                                    .ForPath(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone));
            
            CreateMap<ConsultCommand, ConsultDTO>()
                                    .ForMember(dest => dest.DateConsult, opt => opt.MapFrom(src => src.DateConsult))
                                    .ForMember(dest => dest.Document, opt => opt.MapFrom(src => src.Document))
                                    .ForMember(dest => dest.ServiceID, opt => opt.MapFrom(src => src.ServiceID));
        }
    }
}