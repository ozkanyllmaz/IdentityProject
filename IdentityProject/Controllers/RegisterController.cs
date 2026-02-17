using IdentityProject.Dtos;
using IdentityProject.Entities;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace IdentityProject.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto.Password != userRegisterDto.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Şifreler uyuşmuyor");
                return View(userRegisterDto);
            }
            if (userRegisterDto.AcceptTerms == false)
            {
                ModelState.AddModelError("AcceptTerms", "Kullanım şartlarını kabul etmelisiniz");
                return View(userRegisterDto);
            }

            Random random = new Random();
            string code = random.Next(100000, 1000000).ToString();

            AppUser appUser = new AppUser()
            {
                Name = userRegisterDto.Name,
                Surname = userRegisterDto.Surname,
                UserName = userRegisterDto.Username,
                Email = userRegisterDto.Email,
                AcceptTerms = userRegisterDto.AcceptTerms,
                ConfirmCode = code,
                Address = "Belirtilmedi",
                MyAbout = "Belirtilmedi",

            };


            var result = await _userManager.CreateAsync(appUser, userRegisterDto.Password);
            if (result.Succeeded)
            {
                //mail gönderme
                MimeMessage mimeMessage = new MimeMessage();

                //gonderen
                MailboxAddress mailboxAddressFrom = new MailboxAddress("Mendy Admin", "ozkanyilmaz.dev@gmail.com");
                mimeMessage.From.Add(mailboxAddressFrom);

                //alıcı
                MailboxAddress mailboxAddressTo = new MailboxAddress("User", userRegisterDto.Email);
                mimeMessage.To.Add(mailboxAddressTo);

                mimeMessage.Subject = "Mendy Admin - Doğrulama Kodunuz";

                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $"Kayıt işlemini tamamlamak için doğrulama kodunuz: <strong>{ code }</strong>";

                mimeMessage.Body = bodyBuilder.ToMessageBody();

                //Smtp baqlantısı ve gönderme islemi
                SmtpClient client = new SmtpClient();   
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("ozkanyilmaz.dev@gmail.com", "rneg xous vyts zpuj");

                client.Send(mimeMessage);
                client.Disconnect(true);
                //mail gönderme bitti

                ViewBag.ShowConfirm = true;
                ViewBag.Email = userRegisterDto.Email;

                return View(userRegisterDto);
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(userRegisterDto);
        }


        public async Task<IActionResult> ConfirmEmail(string email, string code)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null && user.ConfirmCode == code)
            {
                user.EmailConfirmed = true;
                user.TwoFactorEnabled = true;
                await _userManager.UpdateAsync(user);
                return RedirectToAction("UserLogin", "Login");
            }
            ViewBag.ShowConfirm = true;
            ViewBag.Email = email;
            ModelState.AddModelError("", "Doğrulama kodu yanlış");

            return View("CreateUser");
        }


    }
}
