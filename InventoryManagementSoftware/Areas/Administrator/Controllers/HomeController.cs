using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSoftware.Web.Areas.Administrator.Controllers
{
    public class HomeController : Controller
    {
        [Area("Administrator")]
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Inventory");
        }
    }
}