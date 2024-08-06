using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLogin.DAL;
using WebLogin.Models;

namespace WebLogin.Controllers
{
    public class AuthenticationController : Controller
    {
        private UserDAL _userDal;

        public AuthenticationController()
        {
            _userDal = new UserDAL();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isValidUser = _userDal.ValidateUser(model.Email, model.Password);

                if (isValidUser)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Login failed! Check your email and password";
                }
            }

            return View(model);
        }
    }
}