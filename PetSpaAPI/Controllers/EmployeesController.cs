using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSpaAPI.DAL;
using PetSpaAPI.DAL.Entities;
using PetSpaAPI.Migrations;

namespace PetSpaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public EmployeesController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet, ActionName("Get")]
        [Route("GetEmployeeById/{employeeId}")]
        public async Task<ActionResult<Employee>> GetEmployeeByCc(int cc)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Cc == cc);
            if(employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPost, ActionName("Create")]
        [Route("CreateEmployee")]
        public async Task<ActionResult> CreateEmployee(Employee employee, int chargeId)
        {
            try
            {
                employee.ChargeId = chargeId;
                employee.Charge = await _context.Charges.FirstOrDefaultAsync(c => c.Id == chargeId);

                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe en {1}.", employee.Name, employee.Charge.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(employee);
        }

        [HttpPut, ActionName("Edit")]
        [Route("EditEmployee/{employeeId}")]
        public async Task<ActionResult> EditEmployee(int employeeCc, Employee employee)
        {
            try
            {
                if (employeeCc != employee.Cc) return NotFound("Employee not found");

                _context.Employees.Update(employee);
                await _context.SaveChangesAsync(); //Aqui se hace el update...
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe en {1}.", employee.Name, employee.Charge.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(employee);
        }

        [HttpDelete, ActionName("Delete")]
        [Route("DeleteEmployee/{employeeId}")]
        public async Task<ActionResult> DeleteEmployee(int employeeCc)
        {
            if (_context.Employees == null) return Problem("Entity set 'DataBaseContext.Employees' is null.");
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Cc == employeeCc);

            if (employee == null) return NotFound("Employee not found");
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(String.Format("El empleado {0} fue eliminado.", employee.Name));
        }
    }
}
