using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> Employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "John Doe", Position = "Developer", Salary = 60000 },
            new Employee { Id = 2, Name = "Jane Smith", Position = "Manager", Salary = 80000 }
        };

        // GET: api/Employee
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Employees);
        }

        // GET: api/Employee/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var employee = Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        // POST: api/Employee
        [HttpPost]
        public IActionResult Create([FromBody] Employee newEmployee)
        {
            newEmployee.Id = Employees.Count > 0 ? Employees.Max(e => e.Id) + 1 : 1;
            Employees.Add(newEmployee);
            return CreatedAtAction(nameof(GetById), new { id = newEmployee.Id }, newEmployee);
        }

        // PUT: api/Employee/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Employee updatedEmployee)
        {
            var employee = Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = updatedEmployee.Name;
            employee.Position = updatedEmployee.Position;
            employee.Salary = updatedEmployee.Salary;

            return NoContent();
        }

        // DELETE: api/Employee/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var employee = Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            Employees.Remove(employee);
            return NoContent();
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
    }
}