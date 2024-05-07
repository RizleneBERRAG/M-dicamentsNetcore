using Médicaments.Models.Dao;
using Médicaments.Models.MesExceptions;
using Médicaments.Models.Metier;
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

		public IActionResult Modifier(string id)

		{
			Prescrire mesPrescriptions = null;

			try
			{

				mesPrescriptions = ServiceMedicament.GetmesPrescriptions(id);
				if (mesPrescriptions == null)
				{
					return NotFound();
				}
				return View(mesPrescriptions);

			}

			catch (MonException e)
			{

				return NotFound();


			}

		}

		[HttpPost]
		public IActionResult Modifier(Prescrire unP)

		{
			try
			{


				ServiceMedicament.UpdatePrescription(unP);

				return RedirectToAction("Index");

			}

			catch (MonException e)
			{

				return View(unP);

			}

		}


		public IActionResult Ajouter()

		{
			var mesPrescriptions = new Prescrire();
				return View(mesPrescriptions);

		}


		[HttpPost]
		public IActionResult Ajouter(Prescrire unP)

		{
			try
			{


				ServiceMedicament.InsertPrescription(unP);

				return RedirectToAction("Index");

			}

			catch (MonException e)
			{

				return View(unP);

			}

		}





	}
}
