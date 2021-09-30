using Data.Entities.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.IServices.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Identity
{
    public class SignInService : ISignInService
    {
        private readonly SignInManager<User> _signInManager;

        //public SignInService(UserManager<User> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<User> claimsFactory,
        //    IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<User>> logger, IAuthenticationSchemeProvider schemes)
        //    : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        //{ }
        public SignInService(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public Task<bool> CanSignInAsync(User user)
        {
            return _signInManager.CanSignInAsync(user);
        }

        public Task<SignInResult> CheckPasswordSignInAsync(User user, string password, bool lockoutOnFailure)
        {
            return _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
        }

        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, string userId = null)
        {
            return _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, userId);
        }

        public Task<ClaimsPrincipal> CreateUserPrincipalAsync(User user)
        {
            return _signInManager.CreateUserPrincipalAsync(user);
        }

        public Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent)
        {
            return _signInManager.ExternalLoginSignInAsync(loginProvider, providerKey, isPersistent);
        }

        public Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent, bool bypassTwoFactor)
        {
            return _signInManager.ExternalLoginSignInAsync(loginProvider, providerKey, isPersistent, bypassTwoFactor);
        }

        public Task ForgetTwoFactorClientAsync()
        {
            return _signInManager.ForgetTwoFactorClientAsync();
        }

        public Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
        {
            return _signInManager.GetExternalAuthenticationSchemesAsync();
        }

        public Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string expectedXsrf = null)
        {
            return _signInManager.GetExternalLoginInfoAsync(expectedXsrf);
        }

        public Task<User> GetTwoFactorAuthenticationUserAsync()
        {
            return _signInManager.GetTwoFactorAuthenticationUserAsync();
        }

        public bool IsSignedIn(ClaimsPrincipal principal)
        {
            return _signInManager.IsSignedIn(principal);
        }

        public Task<bool> IsTwoFactorClientRememberedAsync(User user)
        {
            return _signInManager.IsTwoFactorClientRememberedAsync(user);
        }

        public Task<SignInResult> PasswordSignInAsync(User user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return _signInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }

        public Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return _signInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }

        public Task RefreshSignInAsync(User user)
        {
            return _signInManager.RefreshSignInAsync(user);
        }

        public Task RememberTwoFactorClientAsync(User user)
        {
            return _signInManager.RememberTwoFactorClientAsync(user);
        }

        public Task SignInAsync(User user, bool isPersistent, string authenticationMethod = null)
        {
            return _signInManager.SignInAsync(user, isPersistent, authenticationMethod);
        }

        public Task SignInAsync(User user, AuthenticationProperties authenticationProperties, string authenticationMethod = null)
        {
            return _signInManager.SignInAsync(user, authenticationProperties, authenticationMethod);
        }

        public Task SignOutAsync()
        {
            return _signInManager.SignOutAsync();
        }

        public Task<SignInResult> TwoFactorAuthenticatorSignInAsync(string code, bool isPersistent, bool rememberClient)
        {
            return _signInManager.TwoFactorAuthenticatorSignInAsync(code, isPersistent, rememberClient);
        }

        public Task<SignInResult> TwoFactorRecoveryCodeSignInAsync(string recoveryCode)
        {
            return _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);
        }

        public Task<SignInResult> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberClient)
        {
            return _signInManager.TwoFactorSignInAsync(provider, code, isPersistent, rememberClient);
        }

        public Task<IdentityResult> UpdateExternalAuthenticationTokensAsync(ExternalLoginInfo externalLogin)
        {
            return _signInManager.UpdateExternalAuthenticationTokensAsync(externalLogin);
        }

        public Task<User> ValidateSecurityStampAsync(ClaimsPrincipal principal)
        {
            return _signInManager.ValidateSecurityStampAsync(principal);
        }

        public Task<bool> ValidateSecurityStampAsync(User user, string securityStamp)
        {
            return _signInManager.ValidateSecurityStampAsync(user, securityStamp);
        }

        public Task<User> ValidateTwoFactorSecurityStampAsync(ClaimsPrincipal principal)
        {
            return _signInManager.ValidateTwoFactorSecurityStampAsync(principal);
        }

        //protected Task<bool> IsLockedOut(User user)
        //{
        //    return _signInManager.IsLockedOut(user);
        //}

        //protected Task<SignInResult> LockedOut(User user)
        //{
        //    return _signInManager.LockedOut(user);
        //}

        //protected Task<SignInResult> PreSignInCheck(User user)
        //{
        //    return _signInManager.PreSignInCheck(user);
        //}

        //protected Task ResetLockout(User user)
        //{
        //    return _signInManager.ResetLockout(user);
        //}

        //protected Task<SignInResult> SignInOrTwoFactorAsync(User user, bool isPersistent, string loginProvider = null, bool bypassTwoFactor = false)
        //{
        //    return _signInManager.SignInOrTwoFactorAsync(user, isPersistent, loginProvider, bypassTwoFactor);
        //}
    }
}
