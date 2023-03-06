using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoginAPIwithEF.Models;
using Microsoft.AspNetCore.Cors;

namespace LoginAPIwithEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class StudentsController : ControllerBase
    {
        private readonly APIDbContext _context;
        private readonly ILogger<StudentsController> _logger;
        //controller constructor
        public StudentsController(APIDbContext context, ILogger<StudentsController> logger)
        {
            _context = context;
            _logger = logger;
        }
       
        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            try {
                return await _context.Students.ToListAsync();
            }
           
             catch (Exception ex)
             {
                _logger.LogError(ex, "An error occurred while getting all samples");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while getting all samples. Please try again later.");
            }
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            try {
                var student = await _context.Students.FindAsync(id);

                if (student == null)
                {
                    return NotFound();
                }

                return student;
            }
            
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting sample with ID {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while getting sample with ID {id}. Please try again later.");
            }
        }

        // GET: api/Students/name
   


        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }


        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] Student student)
        {

            if (student == null)
            {
                return BadRequest();
            }
            var user = await _context.Students.FirstOrDefaultAsync(x=>x.Name==student.Name && x.Password==student.Password);
            if (user == null)
            {
                // return NotFound(new { Message = "User Not Found!" });
                return Ok(new { isAuthenticated = false });

            }
            return Ok(new { isAuthenticated = true });
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
