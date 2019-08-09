using System.Collections.Generic;

namespace ProAgil.WebAPI.DTO
{
    public class EventoDTO
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }
        public int QdtPessoa { get; set; }
        public string ImageURL { get; set; }
        public string  Tema { get; set; }
        public string TelefoneContato { get; set; }
        public string Email { get; set; }
        public List<LoteDTO> Lotes { get; set; }
        public List<RedeSocialDTO> RedeSociais { get; set; }
        public List<PalestranteDTO> Palestrantes { get; set; }
    }
}