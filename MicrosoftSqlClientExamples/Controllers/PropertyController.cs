using Microsoft.AspNetCore.Mvc;
using MicrosoftSqlClientExamples.Models;
using MicrosoftSqlClientExamples.Repository.Interfaces;

namespace MicrosoftSqlClientExamples.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController(IPropertyRepository _repo) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _repo.GetAllPropertiesAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var property = await _repo.GetPropertyByIdAsync(id);
            if (property == null) return NotFound();
            return Ok(property);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Property property)
        {
            var result = await _repo.AddPropertyAsync(property);
            return result > 0 ? Ok("Property added successfully") : BadRequest("Insert failed");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Property property)
        {
            property.PropertyId = id;
            var result = await _repo.UpdatePropertyAsync(property);
            return result > 0 ? Ok("Property updated successfully") : NotFound("Property not found");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repo.DeletePropertyAsync(id);
            return result > 0 ? Ok("Property deleted successfully") : NotFound("Property not found");
        }
    }
}
