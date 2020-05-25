using DutchTreat.Models;
using DutchTreat.Services;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private IMailService mailService;
        public AppController(IMailService MailService)
        {
            mailService = MailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewBag.Title = "Contact Us";

            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactUsModel model)
        {
            if (ModelState.IsValid)
            {
                mailService.SendMessage("gleam2k19@gmail.com", model.Email, $"{model.Subject} + Name: {model.Name}",
                    model.Message);
                ViewBag.SentMessage = "Message has been sent!";
                ModelState.Clear();
            }
    
            return View();

        }

        public IActionResult About()
        {
            ViewBag.Title = "About";

            return View();
        }
    }
}
