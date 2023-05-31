using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSpaAPI.DAL;
using PetSpaAPI.DAL.Entities;

namespace PetSpaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChargesController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public ChargesController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet, ActionName("Get")]
        [Route("Get")]

        public async Task<ActionResult<IEnumerable<Charge>>> GetCharges()
        {
            var charges = await _context.Charges.ToListAsync();
            if (charges == null) return NotFound();

            return charges;
        }


        [HttpGet, ActionName("Get")]
        [Route("Get/{id}")]

        public async Task<ActionResult<Charge>> GetChargesById(int id)
        {
            var charges = await _context.Charges.FirstOrDefaultAsync(c => c.Id == id);
            if (charges == null) return NotFound();

            return Ok(charges);
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]

        public async Task<ActionResult> CreateCharge(Charge charge)
        {
            try
            {
                _context.Charges.Add(charge);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", charge.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            return Ok(charge);

        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit/{id}")]

        public async Task<ActionResult> EditCharge(int id, Charge charge)
        {
            try
            {
                if (id != charge.Id) return NotFound("Charge not found");

                charge.Description = _context.Charges.Single().Description;

                _context.Charges.Update(charge);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", charge.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            return Ok(charge);

        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete/{id}")]

        public async Task<ActionResult> DeleteCharge(int id)
        {
            if (_context.Charges == null) return Problem("Entity set 'DataBaseContex.Charges' is null");
            var charge = await _context.Charges.FirstOrDefaultAsync(c => c.Id == id);

            if (charge == null) return NotFound("Charge not found");

            _context.Charges.Remove(charge);
            await _context.SaveChangesAsync();

            return Ok(String.Format("El cargo {0} fue eliminado", charge.Name));

        }

    }
}
