using Microsoft.AspNetCore.Mvc;
using ValuteSalute.Domain;

namespace ValuteSalute.Controllers
{
    public class HomeController
        : Controller
    {
        private readonly DataManager _dataManager;

        public HomeController(DataManager dataManager)
        {
            this._dataManager = dataManager;
        }

        public IActionResult Index()
        {
            return View(this._dataManager.TextFields.GetTextFieldByCodeWord("PageIndex"));
        }

        public IActionResult Contacts()
        {
            return View(this._dataManager.TextFields.GetTextFieldByCodeWord("PageContacts"));
        }
    }
}
