using Zeeyo.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Zeeyo.Service.DTOs.Logins;
using Zeeyo.Service.DTOs.Emails;
using Zeeyo.Service.DTOs.SmsMessages;
using Zeeyo.Service.Interfaces.Users;
using Zeeyo.Service.Interfaces.Accounts;
using Zeeyo.Service.DTOs.Users.UserResetPassword;

namespace Zeeyo.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISmsService _smsService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IAccountService _accountService;

        public AccountController(
            ISmsService smsService,
            IUserService userService,
            IEmailService emailService,
            IAccountService accountService)
        {
            _smsService = smsService;
            _userService = userService;
            _emailService = emailService;
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginForCreationDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _accountService.LoginAsync(model);
            if (result.Token != null)
            {
                // Store the token in a cookie (if using JWT)
                Response.Cookies.Append("AuthToken", result.Token, new CookieOptions
                {
                    HttpOnly = true, // Prevent JavaScript access
                    Secure = true,   // Use HTTPS
                    Expires = DateTime.UtcNow.AddMinutes(1) // Set expiration time
                });

                return RedirectToAction("Index", "AdminPanel");
            }
            else
            {
                ModelState.AddModelError("", "Email or password is incorrect");
                return View(model);
            }
        }


        [HttpGet]
        public IActionResult VerifyEmailOrPhoneNumber()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyEmailOrPhoneNumber(VerifyEmailOrPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userIsFind = await _userService.CheckUserAsync(model.EmailOrPhoneNumber);

            if (userIsFind == true)
            {
                if (!model.EmailOrPhoneNumber.Contains("@"))
                {
                    var codeIsSendToPhone = await _smsService.SendCodeByPhoneNumberAsync(model.EmailOrPhoneNumber);
                }
                else
                {
                    var codeIsSendToEmail = await _emailService.SendCodeByEmailAsync(model.EmailOrPhoneNumber);
                }

                return RedirectToAction("VerifyCode", "Account", new { emailOrPhoneNumber = model.EmailOrPhoneNumber });
            }
            else
            {
                ModelState.AddModelError("", "User not found!");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult VerifyCode(string emailOrPhoneNumber)
        {
            if (string.IsNullOrEmpty(emailOrPhoneNumber))
            {
                return RedirectToAction("VerifyEmailOrPhoneNumber");
            }

            var model = new VerifyCodeViewModel
            {
                EmailOrPhoneNumber = emailOrPhoneNumber
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if(model.EmailOrPhoneNumber.Contains("@"))
            {
                var emailDto = new EmailCreationDto()
                {
                    Email = model.EmailOrPhoneNumber,
                    Code = model.Code
                };
                var userEmailIsVerify = _emailService.VerifyCode(emailDto);

                if (!userEmailIsVerify)
                {
                    ModelState.AddModelError("", "Email verification is failed!");
                    return View(model);
                }
                else
                {
                    return RedirectToAction("ChangePassword", "Account", new { emailOrPhoneNumber = model.EmailOrPhoneNumber });
                }
            }
            else
            {
                var smsDto = new Message()
                {
                    PhoneNumber = model.EmailOrPhoneNumber,
                    Code = model.Code
                };

                var userPhoneIsVerify = _smsService.VerifyCode(smsDto);

                if (!userPhoneIsVerify)
                {
                    ModelState.AddModelError("", "Phone number verifation is failed!");
                    return View(model);
                }
                else
                {
                    return RedirectToAction("ChangePassword", "Account");
                }
            }
        }

        [HttpGet]
        public IActionResult ChangePassword(string emailOrPhoneNumber)
        {
            if (string.IsNullOrEmpty(emailOrPhoneNumber))
            {
                return RedirectToAction("VerifyCode", "Account");
            }

            return View(new ResetPasswordDto { PhoneNumberOrEmail = emailOrPhoneNumber });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ResetPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Email not found!");
                return View(model);
            }

            var result = await _userService.ResetPasswordAsync(model);

            if (result)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ModelState.AddModelError("", "Reset password is failed!");
                return View(model);
            }
        }
    }
}
