using Médicaments.Models.Dao;
using Médicaments.Models.MesExceptions;
using Microsoft.AspNetCore.Mvc;

namespace Médicaments.Controllers
{
    public class MedicamentController : Controller
    {
        public IActionResult Index()
        {
            System.Data.DataTable mesMedicaments = null;

            try
            {
                mesMedicaments = ServiceMedicament.GetTousLesMedicaments();

            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la récupération des médicaments : " + e.Message);
            }

            return View(mesMedicaments);
        }

    }
}
