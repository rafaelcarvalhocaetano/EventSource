using System;
using System.Collections.Generic;


namespace ProAgil.WebAPI.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public DateTime DataEvento { get; set; }
        public int QdtPessoa { get; set; }
        public string ImageURL { get; set; }
        public string  Tema { get; set; }
        public string TelefoneContato { get; set; }
        public string Email { get; set; }
        public List<Lote> Lotes { get; set; }
        public List<RedeSocial> RedeSociais { get; set; }
        public List<PalestranteEvento> PalestranteEventos { get; set; }

    }
}