using System.Web.Mvc;

namespace TesteWay2Joel.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Teste do processo de seleção Way2 Tecnologia";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Meus contatos";

            return View();
        }
    }
}
