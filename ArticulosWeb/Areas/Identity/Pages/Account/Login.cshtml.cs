using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ArticulosWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ArticulosWeb.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Debe completar el campo Email.")]
            [EmailAddress(ErrorMessage = "Debe tener el siguiente formato 'Ejemplo@mail.com'")]
            [Display(Name = "Correo")]
            [StringLength(30, ErrorMessage = "La {0} debe tener minimo {2} y maximo {1} caracteres.", MinimumLength = 6)]
            [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Ejemplo@mail.com")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Debe completar el campo Contraseña.")]
            [StringLength(30, ErrorMessage = "La {0} debe tener minimo {2} y maximo {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W]).{6,}$",
            ErrorMessage = "El campo {0} no cumple con los siguientes: \n " +
            "\n Una o mas letras Mayusculas." +
            "\n Una o mas letras Minisculas." +
            "\n Uno o mas Caracteres especiales como (.*@_-)" +
            "\n uno o mas números del 0 al 9")]
            [Display(Name = "Contraseña")]
            public string Password { get; set; }

            [Display(Name = "¿Recordar datos?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            Console.WriteLine("onGetAsync");
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            Console.WriteLine("onPostAsync");
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Usuario Conectado. ;)");
                    return LocalRedirect(returnUrl).WithSuccess("Bien Hecho!", "Bienvenido al Sistema de POA's.  :)"); ;
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("Cuenta de usuario bloqueada");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Intento de inicio de sesión no válido :'( , \n verifique que sus credenciales sean correctas.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
