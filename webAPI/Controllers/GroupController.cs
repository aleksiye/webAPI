using DataAccess;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Identity.Client;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly TerminContext _context;
        public GroupController()
        {
            _context = new TerminContext();
        }
        // GET: api/<TestController>
        [HttpGet]
        public IActionResult Get()
        {
            var groups = _context.Groups.ToList();
            return Ok(groups);
        }

        // GET api/<TestController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok("probaid");
        }

        // POST api/<TestController>
        [HttpPost]
        public void Post([FromBody] GroupDTO dto)
        {
            var group = new Group
            {
                Name = dto.Name
            };
            _context.Groups.Add(group);
            _context.SaveChanges();
        }

        // PUT api/<TestController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] GroupDTO dto)
        {
            var group = _context.Groups.Find(id);
            if (group == null)
            {
                return NotFound();
            }
            try
            {
                group.Name = dto.Name;
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<TestController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var group = _context.Groups.Find(id);
            if (group == null)
            {
                return NotFound();
            }
            try
            {
                group.IsDeleted = true;
                group.DeletedAt = DateTime.Now;
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }
    }
    public class GroupDTO
    {
        public string Name { get; set; }
    }
}
