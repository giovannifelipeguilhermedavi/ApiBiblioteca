using ApiBiblioteca.ModelViews;
using AutoMapper;
using Biblioteca.Dominio.Entidades;
using Biblioteca.Dominio.Servico.Editoras;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBiblioteca.Controllers.V1
{
    /// <summary>
    /// Controller.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class EditoraController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly EditoraServ editoraServ;

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="mapper">injeção mapper.</param>
        /// <param name="editoraServ">injeção editora.</param>
        public EditoraController(IMapper mapper, EditoraServ editoraServ)
        {
            this.mapper = mapper;
            this.editoraServ = editoraServ;
        }

        /// <summary>
        /// Listar todas as editoras.
        /// </summary>
        /// <returns>Lista.</returns>
        [HttpGet("Listar")]
        public async Task<ActionResult> Listar()
        {
            try
            {
                var lista = this.mapper.Map<List<EditoraViewModel>>(await this.editoraServ.Selecionar());
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
                var editora = this.mapper.Map<EditoraViewModel>(await this.editoraServ.Selecionar(id));
                return this.Ok(editora);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Adicionar uma editora.
        /// </summary>
        /// <param name="model">modelo.</param>
        /// <returns>Codigo 200.</returns>
        [HttpPost("Adicionar")]
        public async Task<ActionResult> Adicionar([FromBody] EditoraViewModel model)
        {
            try
            {
                var editora = this.mapper.Map<Editora>(model);
                var id = await this.editoraServ.Adicionar(editora);
                return this.Ok(id);
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
                await this.editoraServ.Excluir(id);
                return this.Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
