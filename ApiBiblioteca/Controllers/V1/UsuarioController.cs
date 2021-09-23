using ApiBiblioteca.ModelViews;
using AutoMapper;
using Biblioteca.Dominio.Entidades;
using Biblioteca.Dominio.Servico.Usuarios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiBiblioteca.Controllers.V1
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UsuarioServ usuarioServ;

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="mapper">injeção mapper.</param>
        /// <param name="usuarioServ">injeção usuario.</param>
        public UsuarioController(IMapper mapper, UsuarioServ usuarioServ)
        {
            this.mapper = mapper;
            this.usuarioServ = usuarioServ;
        }

        /// <summary>
        /// Listar todos os usuários.
        /// </summary>
        /// <returns>Lista.</returns>
        [HttpGet("Listar")]
        public async Task<ActionResult> Listar()
        {
            try
            {
                var lista = this.mapper.Map<List<UsuarioViewModel>>(await this.usuarioServ.Selecionar());
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
                var usuario = this.mapper.Map<UsuarioViewModel>(await this.usuarioServ.Selecionar(id));
                return this.Ok(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Listar por id.
        /// </summary>
        /// <param name="login">parametro.</param>
        /// <returns>Objeto.</returns>
        [HttpGet("GetUsuario/{login}")]
        public async Task<ActionResult> GetUsuario(string login)
        {
            try
            {
                var usuario = this.mapper.Map<UsuarioViewModel>(await this.usuarioServ.GetUsuario(login));
                return this.Ok(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Adicionar uma usuario.
        /// </summary>
        /// <param name="model">modelo.</param>
        /// <returns>Codigo 200.</returns>
        [HttpPost("Adicionar")]
        public async Task<ActionResult> Adicionar([FromBody] UsuarioViewModel model)
        {
            try
            {
                var usuario = this.mapper.Map<Usuario>(model);
                var id = await this.usuarioServ.Adicionar(usuario);
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
                await this.usuarioServ.Excluir(id);
                return this.Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
