using IdentityProject.Context;
using IdentityProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IdentityProject.ViewComponents
{
    public class Menu : ViewComponent
    {
        private readonly EmailContext _context;
        private readonly UserManager<AppUser> _userManager;

        public Menu(EmailContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            int unreadMessagesCount = 0;
            int starredMessagesCount = 0;

            if (user != null)
            {
                unreadMessagesCount = await _context.Messages
                    .Where(x => x.ReceiverId == user.Id.ToString() && x.IsStatus == false)
                    .CountAsync();

                starredMessagesCount = await _context.Messages
                    .Where(x => x.ReceiverId == user.Id.ToString() && (x.IsReceiverStarred == true || x.IsSenderStarred == true))
                    .CountAsync();
            }

            ViewBag.UnreadMessagesCount = unreadMessagesCount;
            ViewBag.StarredMessagesCount = starredMessagesCount;

            return View();
        }
    }
}
