using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication6.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> UserManager;
        public RoleManager<IdentityRole> RoleManager;
        public IEnumerable<IdentityRole> Roles { get; set; }

        public AdminController(UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            this.UserManager = userManager;
            this.RoleManager = roleManager;
            _context = context;
        }


        // GET: Admin
        public async Task<IActionResult> Index()
        {
            var users = UserManager.Users.Select(user => new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = string.Join(",", UserManager.GetRolesAsync(user).Result.ToArray())
            }).ToList();
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> ShowEdit(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (id == null)
            {
                return BadRequest("User not found");
            }
            var model = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            var user = await UserManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return BadRequest("User not found" + model.Id);
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;
                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }
        public async Task<IActionResult> Delete(string id)
        {
            IdentityUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                await UserManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> Create(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            var model = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email
            };
            var roles = RoleManager.Roles;
            ViewBag.Roles = new SelectList(roles.ToList(), "Id", "Name");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUserRole(UserViewModel u)
        {
            var user = await UserManager.FindByEmailAsync(u.Email);
            if (user == null)
            {
                return BadRequest("User does not exist" + u.Email);
            }

            var name = Convert.ToString(await RoleManager.FindByIdAsync(u.Role));
            if (name == null)
            {
                return BadRequest("Role does not exist" + u.Role);
            }

            await UserManager.AddToRoleAsync(user, name);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> RemoveUserRole(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var roles = await UserManager.GetRolesAsync(user);

            ViewBag.Roles = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");

            var model = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveUserRole(UserViewModel u)
        {
            var user = await UserManager.FindByIdAsync(u.Id);
            if (user == null)
            {
                return BadRequest("User does not exist" + u.Email);
            }
            if (string.IsNullOrEmpty(u.Role))
            {
                return BadRequest("Role is null or empty");
            }

            var result = await UserManager.RemoveFromRoleAsync(user, u.Role);
            if (result.Succeeded)
            {
                // Ruaj ndryshimet
                await _context.SaveChangesAsync();

                return RedirectToAction("Index"); // Shto këtë rresht për redirektim pasi të ketë sukses
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("RemoveUserRole", u);
        }

    }
}
