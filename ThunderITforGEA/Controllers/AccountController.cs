using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using ThunderITforGEA.Models;
using System.Web.Hosting;
using System.Net.Mail;
using System.Net;

namespace ThunderITForGEA.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
       
       
        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
           
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager) { AllowOnlyAlphanumericUserNames = false };
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.login, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    Entities baza = new Entities();
                    baza.AspNetUsers.Find(user.Id).iloscDniBezlogowania = 0;
                    baza.AspNetUsers.Find(user.Id).jezyk = model.language;
                    baza.SaveChanges();               
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser();
                user.Email = model.Email;
                user.UserName = model.nazwisko + "." + model.imie.Substring(0, 2); //todo jakies zabezpieczenie przed imionami na 1 litere
                user.imie = model.imie;
                user.nazwisko = model.nazwisko;
                user.firma = model.firma;
                user.adres = model.adres;
                user.miasto = model.miasto;
                user.kraj = model.kraj;
                user.stanowisko = model.stanowisko;
              //  user.segment = model.segment;
                user.telefon = model.telefon;
              
                switch (model.segment2)
                {
                    case "1":
                        user.segment2 = "Separation Sales";
                        break;
                    case "2":
                        user.segment2 = "Food Processing and Packing Sales";
                        break;
                    case "3":
                        user.segment2 = "Milking and Dairy Farming Sales";
                        break;
                    case "4":
                        user.segment2 = "Flow Components, Compression and Homogenization Sales";
                        break;
                    case "5":
                        user.segment2 = "Dairy Sales ";
                        break;
                    case "6":
                        user.segment2 = "Utilities Sales";
                        break;
                    case "7":
                        user.segment2 = "Food Sales";
                        break;
                    case "8":
                        user.segment2 = "Beverage sales";
                        break;
                    case "9":
                        user.segment2 = "Chemicals Sales";
                        break;
                    case "10":
                        user.segment2 = "Project Execution";
                        break;
                    case "11":
                        user.segment2 = "Service Sales Equipment";
                        break;
                    case "12":
                        user.segment2 = "Service Branch Mazovia";
                        break;
                    case "13":
                        user.segment2 = "Service Support";
                        break;
                    case "14":
                        user.segment2 = "Service Sales Solutions";
                        break;
                    case "15":
                        user.segment2 = "Service Branch Silesia";
                        break;
                    case "16":
                        user.segment2 = "Service Branch Pomerania";
                        break;
                    case "17":
                        user.segment2 = "Local Procurement";
                        break;
                    case "18":
                        user.segment2 = "Local Logistics & Warehouse";
                        break;

                }
                switch (model.segment)
                {
                    case "1":
                        user.segment = "Board";
                        user.segment2 = null;
                        break;
                    case "2":
                        user.segment = "Equipment Sales";
                        break;
                    case "3":
                        user.segment = "Solution Sales";
                        break;
                    case "4":
                        user.segment = "Service";
                        break;
                    case "5":
                        user.segment = "Operational Country Marketing";
                        break;
                    case "6":
                        user.segment = "Q-HSE/Lean Manager";
                        break;
                    case "7":
                        user.segment = "Supply Chain";
                        break;

                }
                user.login = user.UserName; //dla pewności i kompatybilności z wygenerowanym email
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {                
                     await wyslijEmail(user,model.Password);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public async Task wyslijEmail(ApplicationUser user,string haslo)
        {        
                string calyHTML = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Content").ToString() + "/emailWzor.html");
                var message = new MailMessage();
                message.To.Add(new MailAddress(user.Email));  // replace with valid value 
                message.From = new MailAddress("powiadomienie@gea-ewidencja.pl");  // replace with valid value
                message.Subject = "Witamy w serwisie GEA-Ewidencja!";
              calyHTML=  calyHTML.Replace("###LOGIN###", user.UserName);
           calyHTML= calyHTML.Replace("###HASLO###",haslo);
                message.Body = calyHTML;//string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;
                message.Attachments.Add(new Attachment(HostingEnvironment.MapPath("~/Content").ToString() + "/Instrukcja-obsługi.pdf".ToString()));
                var doAdmina = new MailMessage();
                doAdmina.To.Add("mateusz.durajewski@gea.com");  // replace with valid value 
                doAdmina.From = new MailAddress("powiadomienie@gea-ewidencja.pl");  // replace with valid value
                doAdmina.Subject = "Rejestracja nowego użytkownika w GEA-Ewidencja";
                doAdmina.Body = "Login : " + user.UserName + "\r\n" + "Imię i nazwisko : " + user.imie + " " + user.nazwisko;
                doAdmina.IsBodyHtml = false;
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "powiadomienie@gea-ewidencja.pl", 
                        Password = "Gea123Surge!"  
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "ssl0.ovh.net";
                    smtp.Port = 25;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    await smtp.SendMailAsync(doAdmina);
                }
            
           
        }
        [AllowAnonymous]
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.login);
                
                UserManager.RemovePassword(user.Id);

                UserManager.AddPassword(user.Id, model.Password);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }


        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

    
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}