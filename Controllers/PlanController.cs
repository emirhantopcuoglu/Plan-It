using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Plan_It.Models;
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
            var plans = await _planService.GetAllPlansAsync(userId!);
            return View(plans);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new PlanViewModel
            {
                Categories = await GetCategorySelectListAsync()
            };
            SetPriorityViewBag();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlanViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategorySelectListAsync();
                SetPriorityViewBag();
                return View(model);
            }

            var userId = _userManager.GetUserId(User)!;
            var result = await _planService.CreatePlanAsync(model, userId);

            if (!result)
            {
                ModelState.AddModelError("", "Plan oluşturulamadı.");
                model.Categories = await GetCategorySelectListAsync();
                SetPriorityViewBag();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = _userManager.GetUserId(User);
            var plan = await _planService.GetPlanByIdAsync(id, userId!);
            if (plan == null)
                return NotFound();

            var viewModel = new PlanViewModel
            {
                PlanId = plan.PlanId,
                Title = plan.Title,
                Deadline = plan.Deadline,
                CategoryId = plan.CategoryId,
                Status = plan.Status,
                Priority = plan.Priority,
                Categories = await GetCategorySelectListAsync()
            };
            SetPriorityViewBag();
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
                model.Categories = await GetCategorySelectListAsync();
                SetPriorityViewBag();
                return View(model);
            }

            var userId = _userManager.GetUserId(User)!;
            var result = await _planService.UpdatePlanAsync(model, userId);

            if (!result)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User)!;
            var result = await _planService.DeletePlanAsync(id, userId);

            if (!result)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int planId, PlanStatus status)
        {
            var userId = _userManager.GetUserId(User)!;
            await _planService.ChangePlanStatusAsync(planId, userId, status);
            return Ok();
        }

        private async Task<IEnumerable<SelectListItem>> GetCategorySelectListAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return categories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.Name
            });
        }

        private void SetPriorityViewBag()
        {
            ViewBag.Priorities = Enum.GetValues(typeof(PriorityLevel));
        }
    }
}
