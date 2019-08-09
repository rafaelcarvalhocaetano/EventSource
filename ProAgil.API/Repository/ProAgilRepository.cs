using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.WebAPI.Models;

namespace ProAgil.WebAPI.Repository
{
    public class ProAgilRepository : IIRepository
    {
        private readonly ProAgilContext _context;      
        
        public ProAgilRepository(ProAgilContext proContext)
        {
            _context = proContext;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);            
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);            
        }

        public async Task<bool> SaveChengesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Evento[]> FindAllEventoAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(x => x.Lotes).Include(x => x.RedeSociais);

            if (includePalestrantes) {
                query = query.Include(x => x.PalestranteEventos).ThenInclude(p => p.Palestrante);
            }
            query = query.AsNoTracking().OrderByDescending(x => x.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> FindEventoTemaAsync(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos.Include(x => x.Lotes).Include(x => x.RedeSociais);

            if (includePalestrantes) {
                query = query.Include(x => x.PalestranteEventos).ThenInclude(p => p.Palestrante);
            }
            query = query.AsNoTracking().OrderByDescending(x => x.DataEvento).Where(x => x.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Evento> FindByIdEvento(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(x => x.Lotes).Include(x => x.RedeSociais);
            if (includePalestrantes)
                query = query.Include(x => x.PalestranteEventos).ThenInclude(x => x.Palestrante);
            
            query = query.AsNoTracking().OrderByDescending(x => x.DataEvento).Where(x => x.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }

        
        public async Task<Palestrante> FindPalestranteAsync(int palestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(x => x.RedeSociais);

            if (includeEventos) 
                query = query.Include(x => x.PalestranteEventos).ThenInclude(x => x.Evento);

            query = query.AsNoTracking().OrderBy(x => x.Nome).Where(x => x.Id == palestranteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> FindAllPalestranteAsync(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(x => x.RedeSociais);

            if (includeEventos)
                query = query.Include(x => x.PalestranteEventos).ThenInclude(x => x.Evento);

            // query = query.OrderBy(x => x.Nome).Where(x => x.Nome == nome);
            query = query.AsNoTracking().Where(x => x.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}