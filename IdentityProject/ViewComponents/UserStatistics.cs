using IdentityProject.Context;
using IdentityProject.Dtos;
using IdentityProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityProject.ViewComponents
{
    public class UserStatistics : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailContext _context;

        public UserStatistics(UserManager<AppUser> userManager, EmailContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            UserStatisticsDto userStatisticsDto = new UserStatisticsDto();
            if(user != null)
            {
                //gelen toplam mesaj
                userStatisticsDto.TotalIncomingMessages = await _context.Messages
                    .Where(x => x.ReceiverId == user.Id.ToString())
                    .CountAsync();

                //gönderilen toplam mesaj
                userStatisticsDto.TotalOutgoingMessages = await _context.Messages
                    .Where(x => x.SenderId == user.Id.ToString())
                    .CountAsync();

                //okunmamış mesaj
                userStatisticsDto.UnreadMessages = await _context.Messages
                    .Where(x => x.ReceiverId == user.Id.ToString() && x.IsStatus == false)
                    .CountAsync();

                //yıldızlı mesaj
                userStatisticsDto.StarredMessages = await _context.Messages
                    .Where(x => x.ReceiverId == user.Id.ToString() && (x.IsReceiverStarred == true || x.IsSenderStarred == true))
                    .CountAsync();

            }
            return View(userStatisticsDto);
        }
    }
}
