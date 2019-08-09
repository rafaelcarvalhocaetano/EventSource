using System.Threading.Tasks;
using ProAgil.WebAPI.Models;

namespace ProAgil.WebAPI.Repository
{
    public interface IIRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChengesAsync();
        Task<Evento[]> FindAllEventoAsync(bool includePalestrantes);
        Task<Evento[]> FindEventoTemaAsync(string tema, bool includePalestrantes);
        Task<Evento> FindByIdEvento(int eventoId, bool includePalestrantes);

        // PALESTRANTE
        Task<Palestrante[]> FindAllPalestranteAsync(string nome, bool includeEventos);
        Task<Palestrante> FindPalestranteAsync(int palestranteId, bool includeEventos);
    }
}