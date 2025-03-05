using AutoMapper;
using poc_estados_api.Models;
using poc_estados_api.Models.Dtos;

namespace poc_estados_api.EstadosMappers;

public class EstadosMapper: Profile
{
    public EstadosMapper()
    {
        CreateMap<Estado, EstadoDto>().ReverseMap();
        CreateMap<AccionEstado, AccionEstadoDto>().ReverseMap();
        CreateMap<Solicitud, SolicitudDto>()
            .ForMember(dest => dest.Estados, opt => opt.Ignore())
            .ForMember(dest => dest.HistorialEstados, opt => opt.Ignore())
            .ReverseMap();
        
    }
}