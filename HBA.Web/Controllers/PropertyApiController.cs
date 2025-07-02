using HBA.Data.Model;
using HBA.Infrastructure.Repository;
using HBA.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HBA.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PropertyApiController : ControllerBase
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly ICommissionSetupRepository _commissionSetupRepository;

        public PropertyApiController(
            IPropertyRepository propertyRepository,
            IPropertyTypeRepository propertyTypeRepository,
            ICommissionSetupRepository commissionSetupRepository)
        {
            _propertyRepository = propertyRepository;
            _propertyTypeRepository = propertyTypeRepository;
            _commissionSetupRepository = commissionSetupRepository;
        }

        // GET: api/property
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAllProperties()
        {
            var properties = await _propertyRepository.GetAllAsync();
            var commissionSetup = await _commissionSetupRepository.GetAllAsync();

            var result = properties.Select(p => new
            {
                p.Id,
                p.PropertyName,
                PropertyType = p.PropertyType?.Type,
                p.Price,
                p.Location,
                CommissionValue = CommissionHelper.CalculateCommission(p.Price, commissionSetup)
            });

            return Ok(result);
        }

        // POST: api/property
        [HttpPost]
        public async Task<ActionResult> CreateProperty([FromBody] Property property)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _propertyRepository.AddAsync(property);
            return Ok(property);
        }

        // GET: api/property/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetPropertyById(int id)
        {
            var property = await _propertyRepository.GetByIdAsync(id);
            var commissionSetup = await _commissionSetupRepository.GetAllAsync();

            if (property == null)
                return NotFound();

            var result = new
            {
                property.Id,
                property.PropertyName,
                PropertyType = property.PropertyType?.Type,
                property.Price,
                property.Location,
                CommissionValue = CommissionHelper.CalculateCommission(property.Price, commissionSetup)
            };

            return Ok(result);
        }

        // PUT: api/property/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditProperty([FromBody] Property property)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _propertyRepository.GetByIdAsync(property.Id);
            if (existing == null)
                return NotFound();

            await _propertyRepository.UpdateAsync(property);
            return Ok(property);
        }

        // DELETE: api/property/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            var property = await _propertyRepository.GetByIdAsync(id);
            if (property == null)
                return NotFound();

            await _propertyRepository.DeleteAsync(id);
            return Ok("Deleted Successfully");
        }
    }
}
