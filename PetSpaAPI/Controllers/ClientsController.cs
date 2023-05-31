using Microsoft.AspNetCore.Mvc;
using PetSpaAPI.DAL;
using PetSpaAPI.DAL.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace PetSpaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public ClientsController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            var clients = await _context.Clients.ToListAsync(); //select * from categories
            if (clients == null) return NotFound();
            return clients;
        }

        [HttpGet, ActionName("Get")]
        [Route("GetClientById/{clientId}")]
        public async Task<ActionResult<Client>> GetClientById(int clientCc)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Cc == clientCc);
            if (client == null) return NotFound();
            return Ok(client);
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateClient(Client client)
        {
            try
            {
                _context.Clients.Add(client);
                await _context.SaveChangesAsync(); //Aqui se hace el insert into...
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe.", client.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(client);
        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit/{cc}")]
        public async Task<ActionResult> EditClient(int cc, Client client) //El "?" es para indicar que es nulleable
        {
            try
            {
                if (cc != client.Cc) return NotFound("Client not found");

                _context.Clients.Update(client);
                await _context.SaveChangesAsync(); //Aqui se hace el update...
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate")) return Conflict(String.Format("{0} ya existe.", client.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(client);
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete/{cc}")]
        public async Task<ActionResult> DeleteClient(int cc)
        {
            if (_context.Clients == null) return Problem("Entity set 'DataBaseContext.Clients' is null.");
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Cc == cc);

            if (client == null) return NotFound("Client not found");
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return Ok(String.Format("La categoría {0} fue eliminada.", client.Name));
        }
    }
}
