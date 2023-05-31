using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSpaAPI.DAL;
using PetSpaAPI.DAL.Entities;

namespace PetSpaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {

        private readonly DataBaseContext _context;

        public SpeciesController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<Species>>> GetSpecies()
        {
            var species = await _context.Species.ToListAsync();
            if (species == null) return NotFound();

            return species;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get/{id}")]
        public async Task<ActionResult<Species>> GetSpeciesById(int id)
        {
            var species = await _context.Species.FirstOrDefaultAsync(c => c.Id == id);
            if (species == null) return NotFound();

            return Ok(species);
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateSpecies(Species species)
        {
            try
            {
                _context.Species.Add(species);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", species.Id));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            return Ok(species);

        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit/{id}")]
        public async Task<ActionResult> EditSpecies(int id, Species species)
        {
            try
            {
                if (id != species.Id) return NotFound("Species not found");

                _context.Species.Update(species);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", species.Id));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            return Ok(species);

        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete/{id}")]
        public async Task<ActionResult> DeleteSpecies(int id)
        {
            if (_context.Species == null) return Problem("Entity set 'DataBaseContex.Species' is null");
            var species = await _context.Species.FirstOrDefaultAsync(c => c.Id == id);

            if (species == null) return NotFound("Species not found");

            _context.Species.Remove(species);
            await _context.SaveChangesAsync();

            return Ok(String.Format("La especie {0} fue eliminada", species.Name));
        }
    }
}
