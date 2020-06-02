using DutchTreat.Data;
using DutchTreat.Models;
using DutchTreat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private IMailService mailService;
        private IDutchRepository _repository;

        public AppController(IMailService MailService,IDutchRepository repository)
        {
            _repository = repository;
            mailService = MailService;
        }

        public IActionResult Index()
        {
            var results = _repository.GetAllProducts();
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

        [Authorize]
        public IActionResult Shop()
        {
            return View();
        }
    }
}
