using IdentityProject.Context;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProject.ViewComponents
{
    public class CategoryList : ViewComponent
    {
        private readonly EmailContext _context;

        public CategoryList(EmailContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Categories.ToList();
            return View(values);    
        }
    }
}
