using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSpaAPI.DAL;
using PetSpaAPI.DAL.Entities;

namespace PetSpaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedsController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public BreedsController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get")]

        public async Task<ActionResult<IEnumerable<Breed>>> GetBreeds()
        {
            var breeds = await _context.Breeds.ToListAsync();
            if (breeds == null) return NotFound();

            return breeds;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get/{id}")]

        public async Task<ActionResult<Breed>> GetBreedById(int id)
        {
            var breeds = await _context.Breeds.FirstOrDefaultAsync(c => c.Id == id);
            if (breeds == null) return NotFound();

            return Ok(breeds);
        }
        [HttpPost, ActionName("Create")]
        [Route("CreateBreed")]

        public async Task<ActionResult> CreateBreed(Breed breed, int speciesId)
        {
            try
            {
                breed.SpeciesId = speciesId;
                breed.Species = await _context.Species.FirstOrDefaultAsync(c => c.Id == speciesId);

                _context.Breeds.Add(breed);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya exite", breed.Id));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            return Ok(breed);

        }

        [HttpPut, ActionName("Edit")]
        [Route("EditBreed/{breedId")]

        public async Task<ActionResult> EditBreed(int id, Breed breed)
        {
            try
            {
                if (id != breed.Id) return NotFound("Breed not found");


                _context.Breeds.Update(breed);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", breed.Id));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            return Ok(breed);

        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete/{id}")]

        public async Task<ActionResult> DeleteBreed(int id)
        {
            if (_context.Breeds == null) return Problem("Entity set 'DataBaseContex.Breed' is null");
            var breed = await _context.Breeds.FirstOrDefaultAsync(c => c.Id == id);

            if (breed == null) return NotFound("Breed not found");

            _context.Breeds.Remove(breed);
            await _context.SaveChangesAsync();

            return Ok(String.Format("La raza {0} fue eliminada", breed.Name));

        }

    }
}
