using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialContact.UI.Admin;

namespace SocialContact.Areas.Admin.Controllers
{
    //[Authorize]
    [Area("admin")]
    public class DefaultController : Controller
    {
        private readonly AdminService _adminService;
        public DefaultController(AdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
    }
}