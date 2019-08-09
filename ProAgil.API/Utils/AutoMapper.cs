using System.Linq;
using AutoMapper;
using ProAgil.WebAPI.DTO;
using ProAgil.WebAPI.Models;

namespace ProAgil.WebAPI.Utils
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Evento, EventoDTO>()
                .ForMember(dest => dest.Palestrantes, opt => { 
                    opt.MapFrom(src => src.PalestranteEventos.Select(x => x.Palestrante).ToList());
                });

            CreateMap<Palestrante, PalestranteDTO>()
                .ForMember(dest => dest.Eventos, opt => {
                    opt.MapFrom(x => x.PalestranteEventos.Select(j => j.Evento).ToList());
                });
                
            CreateMap<Lote, LoteDTO>();
            CreateMap<RedeSocial, RedeSocialDTO>();
        }
    }
    
}