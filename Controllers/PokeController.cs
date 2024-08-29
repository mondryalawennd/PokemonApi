using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonApi.Data;
using PokemonApi.Models;
using PokemonApi.ViewModels;

namespace PokemonApi.Controllers
{
    [ApiController]
    [Route("v1")]
    public class PokeController : ControllerBase
    {

        /// <summary>
        /// Obter todos os Pokémons
        /// </summary>
        /// <returns>Coleção de Pokémons</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll([FromServices] AppDataContext context)
        {

            var lista = await context.Pokes.AsNoTracking().ToListAsync();
            return Ok(lista);
        }


        /// <summary>
        /// Obter único Pokémon
        /// </summary>
        /// <returns>Pokémon</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [Route("GetbyID/{id}")]
        public async Task<IActionResult> GetById([FromServices] AppDataContext context, [FromRoute] int id)
        {

            var pokeEntity = await context.Pokes
                                           .AsNoTracking()
                                           .FirstOrDefaultAsync(x => x.Id == id);
            return pokeEntity == null
                 ? NotFound()
                 : Ok(pokeEntity);
        }

        /// <summary>
        /// Cadastrar Pokémon mestre
        /// </summary>
        /// <param name="input">Dados do Pokémon</param>
        /// <returns>Objeto recém-criado</returns>
        /// <response code="200">Sucesso</response>
        [HttpPost]
        public async Task<IActionResult> PostPokemonMestre([FromServices] AppDataContext context, [FromBody] CreatePokeViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var poke = new Poke
                {
                    Nome = model.Nome,
                    Idade = model.Idade,
                    CPF = model.CPF,
                    Capturado = false
                };

                await context.Pokes.AddAsync(poke);
                await context.SaveChangesAsync();

                return Created($"v1/todos/{poke.Id}", model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Obter lista com 10 Pokémons
        /// </summary>
        /// <returns>Coleção de Pokémons</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        public async Task<IActionResult> GetBy10([FromServices] AppDataContext context)
        {

            var pokeEntity = await context.Pokes
                                             .AsNoTracking()
                                             .Take(10)
                                             .ToListAsync();
            return pokeEntity == null
                 ? NotFound()
                 : Ok(pokeEntity);
        }

        /// <summary>
        /// Informar qual Pokémon foi capturado
        /// </summary>
        /// <param name="id">Identificador do Pokémon</param>
        /// <returns>Nada.</returns>
        /// <response code="404">Não encontrado.</response>
        [HttpPut("PutPokemonCapturado/{id}")]
        public async Task<IActionResult> PutPokemonCapturado([FromServices] AppDataContext context, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var pokemon = await context.Pokes.FirstOrDefaultAsync(x => x.Id == id);

            if (pokemon == null)
                return NotFound();

            try
            {
                pokemon.Capturado = true;

                context.Pokes.Update(pokemon);
                await context.SaveChangesAsync();
                return Ok(pokemon);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        ///// <summary>
        ///// Obter todos os Pokémons Capturados
        ///// </summary>
        ///// <returns>Coleção de Pokémons</returns>
        ///// <response code="200">Sucesso</response>
        //[HttpGet]
        //public async Task<IActionResult> GetByCapturados([FromServices] AppDataContext context)
        //{

        //    var pokeEntity = await context.Pokes
        //                                     .AsNoTracking()
        //                                     .Where(x=> x.Capturado)
        //                                     .ToListAsync();
        //    return pokeEntity == null
        //         ? NotFound()
        //         : Ok(pokeEntity);
        //}

    }
}
