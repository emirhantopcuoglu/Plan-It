using Microsoft.AspNetCore.Mvc;
using Plan_It.Models;
using Plan_It.Repository;

namespace Plan_It.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository<Category> _categoryRepository;
    }
}