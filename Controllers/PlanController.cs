using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Plan_It.Models;
using Plan_It.Repository;
using Plan_It.Services;

namespace Plan_It.Controllers
{
    [Authorize]
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<IdentityUser> _userManager;

        public PlanController(IPlanService planService, ICategoryService categoryService, UserManager<IdentityUser> userManager)
        {
            _planService = planService;
            _categoryService = categoryService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var plans = await _planService.GetAllPlansAsync(userId);
            return View(plans);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            var viewModel = new PlanViewModel
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                })
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlanViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = (await _categoryService.GetAllCategoriesAsync())
                    .Select(c => new SelectListItem { Value = c.CategoryId.ToString(), Text = c.Name });
                return View(model);
            }

            var userId = _userManager.GetUserId(User);
            var result = await _planService.CreatePlanAsync(model, userId);

            if (!result)
            {
                ModelState.AddModelError("", "Plan oluşturulamadı.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = _userManager.GetUserId(User);
            var plan = await _planService.GetPlanByIdAsync(id, userId);
            if (plan == null)
                return NotFound();

            var categories = await _categoryService.GetAllCategoriesAsync();

            var viewModel = new PlanViewModel
            {
                PlanId = plan.PlanId,
                Title = plan.Title,
                Deadline = plan.Deadline,
                CategoryId = plan.CategoryId,
                Status = plan.Status,
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                })
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PlanViewModel model)
        {
            if (id != model.PlanId)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                model.Categories = (await _categoryService.GetAllCategoriesAsync())
                    .Select(c => new SelectListItem { Value = c.CategoryId.ToString(), Text = c.Name });
                return View(model);
            }

            var userId = _userManager.GetUserId(User);
            var result = await _planService.UpdatePlanAsync(model, userId);

            if (!result)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = _userManager.GetUserId(User);
            var plan = await _planService.GetPlanByIdAsync(id, userId);
            if (plan == null)
                return NotFound();

            return View(plan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _planService.DeletePlanAsync(id, userId);

            if (!result)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
