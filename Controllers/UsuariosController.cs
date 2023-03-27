using Microsoft.AspNetCore.Mvc;
using RpgApi.Data;
using System.Threading.Tasks;
using RpgApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly DataContext _context;

        public UsuariosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                Usuario usr = await _context.Usuarios
                    .FirstOrDefaultAsync(uBusca => uBusca.Id == id);
                return Ok(usr);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GetUser")]
        public async Task<IActionResult> Get(Usuario usr)
        {
            try
            {
                Usuario uRetornado = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.Username == usr.Username && x.Email == usr.Email);
                
                if(uRetornado == null)
                    throw new Exception("Usuario não encontrado");
                return Ok(uRetornado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}