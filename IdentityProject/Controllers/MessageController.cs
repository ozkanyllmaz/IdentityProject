using IdentityProject.Context;
using IdentityProject.Dtos;
using IdentityProject.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace IdentityProject.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly EmailContext _context;
        private readonly UserManager<AppUser> _userManager;

        public MessageController(EmailContext emailContext, UserManager<AppUser> userManager)
        {
            _context = emailContext;
            _userManager = userManager;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Inbox()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            var values = _context.Messages
                .Include(x => x.Sender)
                .Include(x => x.Category)
                .Where(x => x.ReceiverId == user.Id)
                .OrderByDescending(x => x.SendDate)
                .ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult SendMessage()
        {
            List<SelectListItem> categories = (from x in _context.Categories.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.CategoryId.ToString()
                                               }).ToList();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(SendMessageDto sendMessageDto)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var receiverUser = await _userManager.FindByEmailAsync(sendMessageDto.ReceiverEmail);

            if (receiverUser == null)
            {
                ModelState.AddModelError("", "Alıcı bulunamadı.");
                return View(sendMessageDto);
            }

            Message message = new Message()
            {
                SenderId = user.Id,
                ReceiverId = receiverUser.Id,
                Subject = sendMessageDto.Subject,
                CategoryId = sendMessageDto.CategoryId,
                MessageDetail = sendMessageDto.MessageDetail,
                SendDate = DateTime.Now,
                IsStatus = false,
                Attachments = new List<Attachment>()
            };

            if (sendMessageDto.Attachments != null && sendMessageDto.Attachments.Count > 0)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "messageFiles");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                foreach (var file in sendMessageDto.Attachments)
                {
                    if (file.Length > 0)
                    {
                        var extension = Path.GetExtension(file.FileName);
                        var newFileName = Guid.NewGuid() + extension;
                        var location = Path.Combine(folderPath, newFileName);

                        // Klasör yoksa oluştur (Garanti olsun)
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        using (var stream = new FileStream(location, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        Attachment attachment = new Attachment()
                        {
                            FileName = file.FileName,
                            FileUrl = "/messageFiles/" + newFileName,
                            FileType = extension,
                            CreateDate = DateTime.Now,
                            AppUserId = user.Id
                        };

                        message.Attachments.Add(attachment);

                    }
                }
            }

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return RedirectToAction("Sendbox");
        }

        public async Task<IActionResult> Sendbox()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            var values = _context.Messages
                .Include(x => x.Receiver)
                .Include(x => x.Category)
                .Where(x => x.Sender.Id == user.Id)
                .ToList();
            return View(values);
        }

        public async Task<IActionResult> MessageDetails(int id)
        {
            var values = await _context.Messages
                .Include(x => x.Receiver)
                .Include(x => x.Sender)
                .Include(x => x.Category)
                .Include(x => x.Attachments)
                .Where(x => x.MessageId == id)
                .FirstOrDefaultAsync();

            if (values != null)
            {
                values.IsStatus = true;
                await _context.SaveChangesAsync();
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> ChangeStarStatus(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var message = await _context.Messages.FindAsync(id);

            if (message != null)
            {
                if (message.SenderId == user.Id)
                {
                    message.IsSenderStarred = !message.IsSenderStarred;
                }
                else if (message.ReceiverId == user.Id)
                {
                    message.IsReceiverStarred = !message.IsReceiverStarred;
                }

                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }


        public async Task<IActionResult> Starred()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                var values = _context.Messages
                    .Include(x => x.Sender)
                    .Include(x => x.Receiver)
                    .Include(x => x.Category)
                    .Where(x => (x.IsSenderStarred == true && x.SenderId == user.Id) || (x.IsReceiverStarred == true && x.ReceiverId == user.Id))
                    .ToList();
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> ListByCategory(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var category = await _context.Categories.FindAsync(id);
            if (user != null)
            {
                var messages = _context.Messages
                    .Include(x => x.Sender)
                    .Include(x => x.Receiver)
                    .Include(x => x.Category)
                    .Where(x => x.CategoryId == id && (x.SenderId == user.Id || x.ReceiverId == user.Id))
                    .ToList();
                foreach (var message in messages)
                {
                    if (message.SenderId == user.Id)
                    {
                        ViewBag.SenderNotEmpty = true;
                    }
                    if (message.ReceiverId == user.Id)
                    {
                        ViewBag.ReceiverNotEmpty = true;
                    }
                }
                return View(messages);
            }
            return View();
        }

        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await _context.Messages
                .Include(x => x.Attachments)
                .FirstOrDefaultAsync(x => x.MessageId == id);


            if (message != null)
            {
                if (message.Attachments != null && message.Attachments.Any())
                {
                    _context.Attachments.RemoveRange(message.Attachments);
                }

                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Inbox");
        }
    }
}
