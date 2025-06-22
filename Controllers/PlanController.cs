using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Plan_It.Models;
using Plan_It.Repository;
using Plan_It.Data;

namespace Plan_It.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanRepository _planRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PlanController(IPlanRepository planRepository, ICategoryRepository categoryRepository)
        {
            _planRepository = planRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var plans = await _planRepository.GetAllPlans();
            return View(plans);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryRepository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Plan plan)
        {
            if (ModelState.IsValid)
            {
                await _planRepository.CreatePlan(plan);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryRepository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name", plan.CategoryId);

            return View(plan);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var plan = await _planRepository.GetById(id);
            if (plan == null) return NotFound();

            var categories = await _categoryRepository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name", plan.CategoryId);
            return View(plan);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Plan plan)
        {
            if (id != plan.PlanId) return BadRequest();

            if (ModelState.IsValid)
            {
                await _planRepository.UpdatePlan(plan);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryRepository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name", plan.CategoryId);
            return View(plan);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var plan = await _planRepository.GetById(id);
            if (plan == null) return NotFound();

            return View(plan);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _planRepository.DeletePlan(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCompletionStatus(int id, bool isCompleted)
        {
            var plan = await _planRepository.GetById(id);
            if (plan == null) return NotFound();

            plan.IsCompleted = isCompleted;
            await _planRepository.UpdatePlan(plan);

            return Json(new { success = true });
        }
    }
}
