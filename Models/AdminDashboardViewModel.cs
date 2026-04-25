namespace Plan_It.Models
{
    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalPlans { get; set; }
        public int TotalCategories { get; set; }
        public int CompletedPlans { get; set; }
        public int InProgressPlans { get; set; }
        public int OverduePlans { get; set; }

        public IReadOnlyList<UserSummary> RecentUsers { get; set; } = Array.Empty<UserSummary>();
    }

    public class UserSummary
    {
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int PlanCount { get; set; }
        public bool IsAdmin { get; set; }
    }
}
