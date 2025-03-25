using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using EmployeenewAPI.DAL;
using EmployeenewAPI.Models;
using System.Collections.Generic;

namespace EmployeenewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly EmployeeDAL _employeeDAL; // ✅ Corrected type

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;

            // ✅ Properly getting the connection string
            string? connectionString = _configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string is missing or invalid.");
            }

            _employeeDAL = new EmployeeDAL(connectionString); // ✅ Correctly assigning EmployeeDAL
        }

        // ✅ Get all employees
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            return Ok(_employeeDAL.GetAllEmployees());
        }

        // ✅ Add a new employee
        [HttpPost]
        public ActionResult Post([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest(new { message = "Invalid employee data" });
            }

            _employeeDAL.AddEmployee(employee);
            return Ok(new { message = "Employee added successfully" });
        }

        // ✅ Update employee
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Employee employee)
        {
            _employeeDAL.UpdateEmployee(id, employee);
            return Ok(new { message = "Employee updated successfully" });
        }

        // ✅ Delete employee
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _employeeDAL.DeleteEmployee(id);
            return Ok(new { message = "Employee deleted successfully" });
        }
    }
}
