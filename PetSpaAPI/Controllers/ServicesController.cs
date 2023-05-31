using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSpaAPI.DAL;
using PetSpaAPI.DAL.Entities;

namespace PetSpaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {

        private readonly DataBaseContext _context;

        public ServicesController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices()
        {
            var services = await _context.Services.ToListAsync();
            if (services == null) return NotFound();

            return services;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get/{id}")]
        public async Task<ActionResult<Service>> GetServiceById(int id)
        {
            var services = await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
            if (services == null) return NotFound();

            return Ok(services);
        }

        [HttpGet, ActionName("Get")]
        [Route("Get/{name}")]
        public async Task<ActionResult<Service>> GetServiceByName(string name)
        {
            var services = await _context.Services.FirstOrDefaultAsync(s => s.Name == name);
            if (services == null) return NotFound();

            return Ok(services);
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateService(Service service)
        {
            try
            {
                _context.Services.Add(service);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe ", service.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            return Ok(service);
        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit/{id}")]

        public async Task<ActionResult> EditService(int id, Service service)
        {
            try
            {
                if (id != service.Id) return NotFound("Service not found");

                service.Duration = _context.Services.Single().Duration;
                service.Cost = _context.Services.Single().Cost;

                _context.Services.Update(service);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", service.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            return Ok(service);
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete/{id}")]

        public async Task<ActionResult> DeleteService(int id)
        {
            if (_context.Services == null) return Problem("Entity set 'DataBaseContex.Services' is null");
            var service = await _context.Services.FirstOrDefaultAsync(c => c.Id == id);

            if (service == null) return NotFound("Service not found");

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return Ok(String.Format("El servicio {0} fue eliminado", service.Name));

        }


    }
}
