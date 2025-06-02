using Microsoft.AspNetCore.Mvc;
using Sahibinden.Business.Abstract;

namespace Sahibinden.AdminPanel.Controllers
{
    [Route("CategoryFeatures")]
    public class CategoryFeaturesController : Controller
    {
        private readonly ICategoryFeaturesService _categoryFeaturesService;

        public CategoryFeaturesController(ICategoryFeaturesService categoryFeaturesService)
        {
            _categoryFeaturesService = categoryFeaturesService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetCategoryFeatures(int categoryId)
        {
            var features = await _categoryFeaturesService.GetByCategoryIdAsync(categoryId);
            return Json(features.Select(f => new { id = f.Id, name = f.Name, }));
        }
    }
}
