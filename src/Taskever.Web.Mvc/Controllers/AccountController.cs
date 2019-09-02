using System;
using System.Security.Claims;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Authorization;
using Abp.Modules.Core.Mvc.Models;
using Abp.Runtime.Security;
using Abp.UI;
using Abp.Users.Dto;
using Abp.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Recaptcha.Web;
using Recaptcha.Web.Mvc;
using Taskever.Users;
using Taskever.Web.Mvc.Models.Account;
using Taskever.Security.Users;

namespace Taskever.Web.Mvc.Controllers
{
    public class AccountController : TaskeverController
    {
        private readonly ITaskeverUserAppService _userAppService;

        private readonly UserManager _userManager;

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public AccountController(ITaskeverUserAppService userAppService, UserManager userManager)
        {
            _userAppService = userAppService;
            _userManager = userManager;
        }

        public virtual ActionResult Login(string returnUrl = "", string loginMessage = "")
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }

            ViewBag.ReturnUrl = returnUrl;
            ViewBag.LoginMessage = loginMessage;
            return View();
        }

        [HttpPost]
        public virtual async Task<JsonResult> Login(LoginModel loginModel, string returnUrl = "")
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException("Your form is invalid!");
            }

            var user = await _userManager.FindAsync(loginModel.EmailAddress, loginModel.Password);
            if (user == null)
            {
                throw new UserFriendlyException("Invalid user name or password!");
            }

            await SignInAsync(user, loginModel.RememberMe);

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }

            return Json(new AjaxResponse { TargetUrl = returnUrl });
        }

        private async Task SignInAsync(TaskeverUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddClaim(new Claim(AbpClaimTypes.TenantId, "42"));
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identity);
        }

        public ActionResult ConfirmEmail(ConfirmEmailInput input)
        {
            _userAppService.ConfirmEmail(input);
            return RedirectToAction("Login", new { loginMessage = "Congratulations! Your account is activated. Enter your email address and password to login" });
        }

        [AbpAuthorize]
        public virtual ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult ActivationInfo()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Register(RegisterUserInput input)
        {
            //TODO: Return better exception messages!
            //TODO: Show captcha after filling register form, not on startup!

            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException("Your form is invalid!");
            }

            var recaptchaHelper = this.GetRecaptchaVerificationHelper();
            if (String.IsNullOrEmpty(recaptchaHelper.Response))
            {
                throw new UserFriendlyException("Captcha answer cannot be empty.");
            }

            var recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();
            if (recaptchaResult != RecaptchaVerificationResult.Success)
            {
                throw new UserFriendlyException("Incorrect captcha answer.");
            }

            input.ProfileImage = ProfileImageHelper.GenerateRandomProfileImage();

            _userAppService.RegisterUser(input);

            return Json(new AjaxResponse { TargetUrl = Url.Action("ActivationInfo") });
        }

        public JsonResult SendPasswordResetLink(SendPasswordResetLinkInput input)
        {
            _userAppService.SendPasswordResetLink(input);

            return Json(new AjaxResponse());
        }

        [HttpGet]
        public ActionResult ResetPassword(int userId, string resetCode)
        {
            return View(new ResetPasswordViewModel { UserId = userId, ResetCode = resetCode });
        }

        [HttpPost]
        public JsonResult ResetPassword(ResetPasswordInput input)
        {
            var recaptchaHelper = this.GetRecaptchaVerificationHelper();
            if (String.IsNullOrEmpty(recaptchaHelper.Response))
            {
                throw new UserFriendlyException("Captcha answer cannot be empty.");
            }

            var recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();
            if (recaptchaResult != RecaptchaVerificationResult.Success)
            {
                throw new UserFriendlyException("Incorrect captcha answer.");
            }

            _userAppService.ResetPassword(input);

            return Json(new AjaxResponse { TargetUrl = Url.Action("Login") });
        }

        [Authorize]
        public JsonResult KeepSessionOpen()
        {
            return Json(new AjaxResponse());
        }
    }
}
