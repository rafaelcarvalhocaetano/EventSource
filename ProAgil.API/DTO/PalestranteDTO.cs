using System.Collections.Generic;

namespace ProAgil.WebAPI.DTO
{
    public class PalestranteDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string MiniCurriculo { get; set; }
        public string ImageURL { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public List<RedeSocialDTO> RedeSociais { get; set; }
        public List<EventoDTO> Eventos { get; set; }
    }
}