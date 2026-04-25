using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Plan_It.Areas.Identity.Data;
using Plan_It.Models;

namespace Plan_It.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var today = DateTime.UtcNow.Date;

            var totalUsers = await _userManager.Users.CountAsync();
            var totalPlans = await _context.Plans.CountAsync();
            var totalCategories = await _context.Categories.CountAsync();
            var completed = await _context.Plans.CountAsync(p => p.Status == PlanStatus.Completed);
            var inProgress = await _context.Plans.CountAsync(p => p.Status == PlanStatus.InProgress);
            var overdue = await _context.Plans
                .CountAsync(p => p.Deadline < today
                                 && p.Status != PlanStatus.Completed
                                 && p.Status != PlanStatus.Cancelled);

            var users = await _userManager.Users
                .AsNoTracking()
                .OrderByDescending(u => u.Id)
                .Take(20)
                .ToListAsync();

            var planCounts = await _context.Plans
                .AsNoTracking()
                .GroupBy(p => p.UserId)
                .Select(g => new { UserId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.UserId, x => x.Count);

            var summaries = new List<UserSummary>(users.Count);
            foreach (var u in users)
            {
                summaries.Add(new UserSummary
                {
                    UserId = u.Id,
                    Email = u.Email ?? "—",
                    PlanCount = planCounts.GetValueOrDefault(u.Id, 0),
                    IsAdmin = await _userManager.IsInRoleAsync(u, "Admin")
                });
            }

            return View(new AdminDashboardViewModel
            {
                TotalUsers = totalUsers,
                TotalPlans = totalPlans,
                TotalCategories = totalCategories,
                CompletedPlans = completed,
                InProgressPlans = inProgress,
                OverduePlans = overdue,
                RecentUsers = summaries
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleAdmin(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var currentUserId = _userManager.GetUserId(User);
            if (userId == currentUserId)
            {
                TempData["Error"] = "Kendi admin yetkini değiştiremezsin.";
                return RedirectToAction(nameof(Index));
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            if (await _userManager.IsInRoleAsync(user, "Admin"))
                await _userManager.RemoveFromRoleAsync(user, "Admin");
            else
                await _userManager.AddToRoleAsync(user, "Admin");

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var currentUserId = _userManager.GetUserId(User);
            if (userId == currentUserId)
            {
                TempData["Error"] = "Kendi hesabını silemezsin.";
                return RedirectToAction(nameof(Index));
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            // Cascade: remove user's plans
            var userPlans = await _context.Plans.Where(p => p.UserId == userId).ToListAsync();
            _context.Plans.RemoveRange(userPlans);
            await _context.SaveChangesAsync();

            await _userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }
    }
}
