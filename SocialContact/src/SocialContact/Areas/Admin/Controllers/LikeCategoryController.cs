using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SocialContact.Areas.Admin.Controllers
{
    [Area("admin")]
    //[Authorize]
    public class LikeCategoryController : Controller
        {
            public IActionResult Index()
            {
                return View();
            }
        }
    }