using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBank.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyBank.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment env;
        public UserController(ApplicationDbContext db,
            IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        public FileContentResult Photo(string userName)
        {

            var user = db.Users.FirstOrDefault(x => x.UserName == userName);
            if (user.ProfilePicture == null)
            {
                return null;
            }
            return new FileContentResult(user.ProfilePicture, "image/jpeg");
        }

        [HttpGet]
        public ActionResult Profile()
        {
            ViewBag.Message = "Update your profile";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile(IFormFile Profile)
        {


            var userName = User.Identity.Name;
            var user = db.Users.Where(x => x.UserName == userName).FirstOrDefault();


            byte[] image = new byte[Profile.Length];
            var resultInBytes = ConvertToBytes(Profile);
            Array.Copy(resultInBytes, image, resultInBytes.Length);
            user.ProfilePicture = image;

            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


        private byte[] ConvertToBytes(IFormFile image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.OpenReadStream().CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
