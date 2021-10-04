using Web.Models.Account;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.IServices.Identity;
using Shared.Common;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Shared.Models.API;
using Services.Services;

namespace Web.Controllers.Base
{
    /// <summary>
    /// Account manangement
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IUserService _userManager;
        private readonly ISignInService _signInManager;

        /// <summary>
        /// Construct services
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public AccountController(IUserService userManager, ISignInService signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Login / Regiser / Log out


        /// <summary>
        /// Get Login user
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(model);
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            //TOdo
            // login functionality
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (user != null)
            {

                if (!user.EmailConfirmed)
                {
                    ModelState.AddModelError(string.Empty, "User Not exists");
                    //return RedirectToAction("Login", loginViewModel);
                }

                if (user.LockoutEnd != null)
                {
                    ModelState.AddModelError(string.Empty, "User temporary locked");
                    //return RedirectToAction("Login", loginViewModel);
                }

                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, ApplicationConstants.LockoutOnFailure);

                if (result.Succeeded)
                {
                    // Get the roles for the user
                    if (loginViewModel.ReturnUrl != null && !string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                    {
                        return Redirect(loginViewModel.ReturnUrl);
                    }


                    if (await _userManager.IsInRoleAsync(user, "Guest"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }
                    if (await _userManager.IsInRoleAsync(user, "User"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }
                    if (await _userManager.IsInRoleAsync(user, "Manager"))
                    {
                        return RedirectToAction("Index", "Home", new { Area = "Admin" });
                        //return Redirect("~/Admin/Home/Index");
                    }
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Index", "Home", new { Area = "Admin" });
                        //return Redirect("~/Admin/Home/Index");
                    }
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "User Not exists");

            if (loginViewModel == null)
            {
                loginViewModel = new LoginViewModel();
            }

            loginViewModel.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            return View("Login", loginViewModel);
        }

        /// <summary>
        /// Get new user Registion page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="registerViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            // register functionality
            var user = new User()
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email,
                FirstName = registerViewModel.Name
                //FirstName = registerViewModel.FirstName,
                //LastName = registerViewModel.LastName,
                //PhoneNumber = registerViewModel.PhoneNumber,
                //TwoFactorEnabled = registerViewModel.TwoFactorEnabled,
            };
            //TODO handle error where register or add user but not expand that before insert
            var result = await _userManager.CreateAsync(user, registerViewModel.Password);

            if (result != null && result.Succeeded)
            {
                var userResult = await _signInManager.PasswordSignInAsync(user, registerViewModel.Password, false, false);

                if (userResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            //TODO handle un authorised
            return RedirectToAction("Login");
        }

        /// <summary>
        /// Logout user
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Error and Access denide

        /// <summary>
        /// Error page
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Access denies
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult AccessDenied()
        {
            return View();
        }

        #endregion

        #region External logins

        /// <summary>
        /// External login for facebook
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            var reduredUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, reduredUrl);

            return new ChallengeResult(provider, properties);
        }

        /// <summary>
        /// External login call back
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="remoteError"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Action("Index", "Home");

            LoginViewModel loginViewModel = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, string.Format("Error from External provider {0}", remoteError));

                return View("Login", loginViewModel);
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, string.Format("Error loading external login information"));

                return View("Login", loginViewModel);
            }

            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    var user = await _userManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        user = new User()
                        {
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email)
                        };
                        await _userManager.CreateAsync(user);
                    }
                    await _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }

                ViewBag.ErrorTitle = string.Format("Email claim not recived from {0}", info.LoginProvider);
                ViewBag.ErrorMessage = string.Format("Please contact on support.");

                return View("Error");
            }
        }

        #endregion

        #region Reset password
        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="token"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            try
            {
                if (token == null || email == null)
                {
                    ModelState.AddModelError("message", "Invalid Token");
                }
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.Email = email;
                    model.Email = token;
                    return View(model);

                }
                else
                {
                    ModelState.AddModelError("message", "Invalid user email");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ex.Log();
                ModelState.AddModelError("message", "Invalid Token");
                return View();
            }
        }

        /// <summary>
        /// Reset password if forgot
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    bool isPassCorrect = (await _userManager.CheckPasswordAsync(user, model.Password));
                    if (isPassCorrect == false)
                    {
                        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

                        if (result.Succeeded)
                        {
                            if (await _userManager.IsLockedOutAsync(user))
                            {
                                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
                            }

                            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                            await _userManager.SaveToken(user.Id, "", true);
                            return View();
                        }
                        else
                        {
                            ModelState.AddModelError("message", result.Errors.FirstOrDefault().Description);
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("message", "Old Password and New Password cannot be same");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid Login Attempt");
                    return View(model);
                }
            }
            return View(model);
        }

        #endregion

        #region Public pages

        /// <summary>
        /// Site Term and condition
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> TermsAndCondition()
        {
            return View();
        }

        /// <summary>
        /// site privacy policy
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> PrivacyPolicy()
        {
            return View();
        }

        /// <summary>
        /// Site support page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Support()
        {
            return View();
        }

        #endregion
    }
}