using ApiBiblioteca.ModelViews;
using AutoMapper;
using Biblioteca.Dominio.Entidades;
using Biblioteca.Dominio.Servico.Editoras;
using Biblioteca.Dominio.Servico.Livros;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiBiblioteca.Controllers.V1
{
    /// <summary>
    /// Controller.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly LivroServ livroServ;

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="mapper">injeção mapper.</param>
        /// <param name="livroServ">injeção livro.</param>
        public LivroController(IMapper mapper, LivroServ livroServ)
        {
            this.mapper = mapper;
            this.livroServ = livroServ;
        }

        /// <summary>
        /// Listar todas os Livros.
        /// </summary>
        /// <returns>Lista.</returns>
        [HttpGet("Listar")]
        public async Task<ActionResult> Listar()
        {
            try
            {
                var lista = this.mapper.Map<List<LivroViewModel>>(await this.livroServ.Selecionar());
                return this.Ok(lista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Listar por id.
        /// </summary>
        /// <param name="id">parametro.</param>
        /// <returns>Objeto.</returns>
        [HttpGet("ListarPorId/{id:int}")]
        public async Task<ActionResult> ListarPorId(int id)
        {
            try
            {
                var livro = this.mapper.Map<LivroViewModel>(await this.livroServ.Selecionar(id));
                return this.Ok(livro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Adicionar uma livro.
        /// </summary>
        /// <param name="model">modelo.</param>
        /// <returns>Codigo 200.</returns>
        [HttpPost("Adicionar")]
        public async Task<ActionResult> Adicionar([FromBody] LivroViewModel model)
        {
            try
            {
                var existe = await this.livroServ.ExisteLivro(model.Nome, model.Autor);
                if (!existe)
                {
                    var livro = this.mapper.Map<Livro>(model);
                    var id = await this.livroServ.Adicionar(livro);
                    return this.Ok(id);
                }
                else
                {
                    return BadRequest("Já existe um livro cadastrado com o mesmo nome e autor.");
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Excluir registro por id.
        /// </summary>
        /// <param name="id">parametro.</param>
        /// <returns>codigo 200.</returns>
        [HttpDelete("Excluir/{id:int}")]
        public async Task<ActionResult> Excluir(int id)
        {
            try
            {
                await this.livroServ.Excluir(id);
                return this.Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
