using Microsoft.AspNetCore.Mvc;
using Plan_It.Models;
using Plan_It.Repository;

namespace Plan_It.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanRepository<Plan> _planRepository;
        public PlanController(IPlanRepository<Plan> planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(_planRepository.GetAllPlans());
        }

        [HttpGet]
        public IActionResult Create()
        {
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

            return View(plan);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var p = await _planRepository.GetById(id);

            if (p == null)
            {
                return NotFound();
            }

            return View(p);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Plan plan)
        {
            if (id != plan.PlanId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _planRepository.UpdatePlan(plan);
                return RedirectToAction(nameof(Index));
            }

            return View(plan);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _planRepository.DeletePlan(id);
            return RedirectToAction(nameof(Index));
        }
    }
}