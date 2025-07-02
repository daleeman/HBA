using HBA.Data.Model;
using HBA.Infrastructure.Repository;
using HBA.Web.Models;
using HBA.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HBA.Web.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly ICommissionSetupRepository _commissionSetupRepository;

        public PropertyController(IPropertyRepository propertyRepository, IPropertyTypeRepository propertyTypeRepository, ICommissionSetupRepository commissionSetupRepository)
        {
            _propertyRepository = propertyRepository;
            _propertyTypeRepository = propertyTypeRepository;
            _commissionSetupRepository = commissionSetupRepository;
        }

        [Authorize(Roles = "Broker,Seeker")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var properties = await _propertyRepository.GetAllAsync();
            var commissionSetup = await _commissionSetupRepository.GetAllAsync();
            var propertyTypes = await _propertyTypeRepository.GetAllAsync();

            ViewBag.PropertyTypes = propertyTypes.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Type
            });

            List<PropertyViewModel> viewModel = properties.Select(x => new PropertyViewModel
            {
                Id = x.Id,
                PropertyName = x.PropertyName,
                PropertyTypeName = x.PropertyType!.Type,
                Price = x.Price,
                Location = x.Location,
                CommissionValue = CommissionHelper.CalculateCommission(x.Price, commissionSetup)
            }).ToList();

            return View(viewModel);


        }
        [Authorize(Roles = "Broker,Seeker")]
        [HttpGet]
        public async Task<IActionResult> FilteredProperties(decimal? minPrice, decimal? maxPrice, int propertyTypeId)
        {

            var properties = await _propertyRepository.GetAllAsync();
            var commissionSetup = await _commissionSetupRepository.GetAllAsync();

            if (minPrice.HasValue)
                properties = properties.Where(p => p.Price >= minPrice.Value).ToList();

            if (maxPrice.HasValue)
                properties = properties.Where(p => p.Price <= maxPrice.Value).ToList();

            if (propertyTypeId > 0)
                properties = properties.Where(p => p.PropertyTypeId == propertyTypeId).ToList();

            var model = properties.Select(x => new PropertyViewModel
            {
                Id = x.Id,
                PropertyName = x.PropertyName,
                PropertyTypeName = x.PropertyType!.Type,
                Price = x.Price,
                Location = x.Location,
                CommissionValue = CommissionHelper.CalculateCommission(x.Price, commissionSetup)
            }).ToList();

            return PartialView("_PropertyList", model);
        }


        [Authorize(Roles = "Broker")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var propertyTypes = await _propertyTypeRepository.GetAllAsync();

            ViewBag.PropertyTypes = propertyTypes.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Type
            });
            return View();
        }

        [Authorize(Roles = "Broker")]
        [HttpPost]
        public async Task<IActionResult> Create(Property property)
        {
            if (ModelState.IsValid)
            {
                await _propertyRepository.AddAsync(property);
                return RedirectToAction("Index");
            }
            return View(property);
        }
        [Authorize(Roles = "Broker")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var property = await _propertyRepository.GetByIdAsync(id);
            var propertyTypes = await _propertyTypeRepository.GetAllAsync();

            ViewBag.PropertyTypes = propertyTypes.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Type
            });

            if (property == null) return View("Error");

            return View(property);
        }

        [Authorize(Roles = "Broker")]
        [HttpPost]
        public async Task<IActionResult> Edit(Property property)
        {
            if (ModelState.IsValid)
            {
                var result = await _propertyRepository.GetByIdAsync(property.Id);
                if (result == null)
                    return View("Error");

                await _propertyRepository.UpdateAsync(property);
                return RedirectToAction("Index");
            }

            return View(property);
        }

        [Authorize(Roles = "Broker")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _propertyRepository.DeleteAsync(id);
            return Json(new { success = true });
        }


    }

}
