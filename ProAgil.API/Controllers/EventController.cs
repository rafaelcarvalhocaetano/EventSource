using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.WebAPI.DTO;
using ProAgil.WebAPI.Models;
using ProAgil.WebAPI.Repository;

namespace ProAgil.API.Controllers
{
    [Route("v1/evento")]
    [ApiController]    
    public class EventController : ControllerBase
    {
        private readonly IIRepository _repository;

        private readonly IMapper _mapper;


        public EventController(IIRepository repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            try
            {
                var eventos = await _repository.FindAllEventoAsync(true);
                var results = _mapper.Map<IEnumerable<EventoDTO>>(eventos);
                return Ok(results);
            }
            catch (System.Exception se)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro na listagem do Service : {se.Message}");
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                var result = await _repository.FindByIdEvento(Id, true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro na listagem por ID...");
            }
        }

        [HttpGet("tema/{tema}")]

        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                var result = await _repository.FindEventoTemaAsync(tema, true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Não foi possível listar os Temas....");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Evento evento) 
        {
            try
            {
                _repository.Add(evento);
                if (await _repository.SaveChengesAsync())
                    return Created($"/v1/evento/{evento.Id}", evento);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Não foi possível salvar essa merda....");
            }
            return BadRequest("Erro ao salvar");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento evento) 
        {
            try
            {
                var ev = await _repository.FindByIdEvento(id, false);
                
                if (ev == null) return NotFound();

                _repository.Update(evento);

                if (await _repository.SaveChengesAsync())
                    return Created($"/v1/evento/{evento.Id}", evento);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Não foi possível Atualizar essa merda....");
            }
            return BadRequest("Erro ao Atualizar");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            try
            {
                var ev = await _repository.FindByIdEvento(id, false);
                
                if (ev == null) 
                    return NotFound();

                _repository.Delete(ev);
                
                if (await _repository.SaveChengesAsync())
                    return Ok();
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Não foi possível Deletar essa merda....");
            }
            return BadRequest("Erro ao Deletar");
        }
    }
}
// cria o arquivo de migrations com o nome de init
// dotnet ef migrations add init
// -----------------------------------------------
// Cria o arquivo com todas as tabelas
// dotnet ef database update