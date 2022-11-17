

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using xyzpharmacy.Data;
using xyzpharmacy.Data.Static;
using xyzpharmacy.Data.ViewModels;
using xyzpharmacy.Models;

namespace xyzpharmacy.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            
            if (user == null)
            {
                ViewBag.Message = $"User {id} cannot be found";
                return View("NotFound");
            }
            else
            {

                var userclaims = await _userManager.GetClaimsAsync(user);
                var userroles = await _userManager.GetRolesAsync(user);

                var model = new EditUserViewModel()
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    EmailAddress = user.Email,
                    Contact = user.Contact,
                    Age = user.Age,
                    DateOfBirth = user.DateOfBirth,
                    Gender = user.Gender,
                    Claims = userclaims.Select(c => c.Value).ToList(),
                    Roles = userroles

                };
                return View(model);

            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.Message = $"User {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Users");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("Users");
                

            }
        }
        
        
        public IActionResult Login() => View(new LoginVM());

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Products");
                    }
                }
                TempData["Error"] = "Incorrect Password. Please, try again!";
                return View(loginVM);
            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(loginVM);
        }

        public IActionResult Register() => View(new RegisterVM());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerVM);
            }

            var newUser = new ApplicationUser()
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress,
                Contact = registerVM.Contact,
                Age = registerVM.Age,
                DateOfBirth = registerVM.DateOfBirth,
                Gender = registerVM.Gender
                
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return View("RegisterCompleted");
        }

        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.Message = $"User {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var userclaims = await _userManager.GetClaimsAsync(user);
                var userroles = await _userManager.GetRolesAsync(user);

                var model = new EditUserViewModel()
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    EmailAddress = user.Email,
                    Contact = user.Contact,
                    Age = user.Age,
                    DateOfBirth = user.DateOfBirth,
                    Gender = user.Gender,
                    Claims = userclaims.Select(c => c.Value).ToList(),
                    Roles = userroles

                };
                return View(model);


            }
            
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                ViewBag.Message = $"User {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                user.FullName = model.FullName;
                user.Contact = model.Contact;
                user.Age = model.Age;
                user.DateOfBirth = model.DateOfBirth;
                user.Email = model.EmailAddress;
                user.Gender = model.Gender;
                user.UserName = model.EmailAddress;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Products");

                }
                foreach(var errors in result.Errors)
                {
                    ModelState.AddModelError("", errors.Description);
                }
                return View(model);
                
            }
            
        }

        public IActionResult ForgotPassword()
        {
                
            return View();


        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.EmailAddress);
            if (user == null)
            {
                ViewBag.Message = $"User {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                if((user.Contact == model.Contact)&& (user.DateOfBirth == model.DateOfBirth))
                {
                    var result = await _userManager.RemovePasswordAsync(user);
                    if (result.Succeeded)
                    {
                        result = await _userManager.AddPasswordAsync(user, model.Password);
                        return RedirectToAction("Index", "Products");

                    }
                    foreach (var errors in result.Errors)
                    {
                        ModelState.AddModelError("", errors.Description);
                    }
                    
                }
                return View(model);


            }

        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Products");
        }
        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }

       
    }
}
