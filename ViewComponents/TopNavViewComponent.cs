using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.ViewComponents
{
    public class TopNavViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public TopNavViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var collection = await _context.Categories.ToListAsync();
            return View(collection.OrderBy(elem => elem.Title));
        }
    }
}
