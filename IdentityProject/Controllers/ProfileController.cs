using IdentityProject.Dtos;
using IdentityProject.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProject.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> UserProfile()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditDto userEditDto = new UserEditDto();    
            userEditDto.Name = values.Name;
            userEditDto.Surname = values.Surname;
            userEditDto.Email = values.Email;
            userEditDto.ImageUrl = values.ImageUrl;
            userEditDto.Address = values.Address;
            userEditDto.MyAbout = values.MyAbout;
            userEditDto.PhoneNumber = values.PhoneNumber;
            userEditDto.About = values.About;
            return View(userEditDto);
        }

        [HttpPost]
        public async Task<IActionResult> UserProfile(UserEditDto userEditDto)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            // Standart güncellemeler
            user.Name = userEditDto.Name;
            user.Surname = userEditDto.Surname;
            user.Email = userEditDto.Email;
            user.Address = userEditDto.Address;
            user.MyAbout = userEditDto.MyAbout;
            user.PhoneNumber = userEditDto.PhoneNumber;
            user.About = userEditDto.About;

            // --- DÜZELTME: Resim seçildiyse işlem yap ---
            if (userEditDto.Image != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(userEditDto.Image.FileName);
                var imagename = Guid.NewGuid() + extension;
                var saveLocation = resource + "/wwwroot/images/" + imagename;

                using (var stream = new FileStream(saveLocation, FileMode.Create))
                {
                    await userEditDto.Image.CopyToAsync(stream);
                }

                user.ImageUrl = "/images/" + imagename;
            }
            
            if (!string.IsNullOrEmpty(userEditDto.Password))
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userEditDto.Password);
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("UserProfile", "Profile");
            }
            return View(userEditDto);
        }


    }
}
