using System.Data;
using Médicaments.Models.MesExceptions;
using Médicaments.Models.Persistance;
using Médicaments.Models.Metier;


namespace Médicaments.Models.Dao
{
	public class ServiceMedicament
	{
		public static DataTable GetTousLesMedicaments()
		{
			DataTable mesMedicaments;
			Serreurs er = new Serreurs("Erreur sur la lecture des médicaments.", "ServiceMedicament.GetTousLesMedicaments()");
			try
			{
				String req = "SELECT medicament.id_medicament, medicament.id_famille, medicament.depot_legal, medicament.nom_commercial, medicament.effets, medicament.contre_indication, medicament.prix_echantillon, famille.lib_famille, dosage.qte_dosage, dosage.unite_dosage, prescrire.posologie, type_individu.lib_type_individu "
					+ " FROM medicament "
					+ " JOIN famille ON medicament.id_famille = famille.id_famille "
					+ " JOIN prescrire ON medicament.id_medicament = prescrire.id_medicament "
					+ " JOIN dosage ON prescrire.id_dosage = dosage.id_dosage "
					+ " JOIN type_individu ON prescrire.id_type_individu = type_individu.id_type_individu ";

				mesMedicaments = DBInterface.Lecture(req, er);
			}
			catch (MonException e)
			{
				throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
			}
			return mesMedicaments;
		}



		public static Prescrire GetmesPrescriptions(String id)
		{
			DataTable dt;
			Prescrire mesPrescriptions = null;
			Serreurs er = new Serreurs("Erreur sur lecture des médicaments.", "Medicament");
			try
			{
				String mysql = "SELECT id_medicament, id_dosage, id_type_individu, posologie ";
				mysql += "FROM prescrire ";
				mysql += "WHERE id_medicament = " + id;
				dt = DBInterface.Lecture(mysql, er);
				if (dt.IsInitialized && dt.Rows.Count > 0)
				{
					mesPrescriptions = new Prescrire();
					DataRow dataRow = dt.Rows[0];
					mesPrescriptions.Id_medicament = int.Parse(dataRow[0].ToString());
					mesPrescriptions.Id_dosage = int.Parse(dataRow[1].ToString());
					mesPrescriptions.Id_type_individu = int.Parse(dataRow[2].ToString());
					mesPrescriptions.Posologie = dataRow[3].ToString();

					return mesPrescriptions;
				}

				else
					return null;
			}
			catch (MonException e)
			{
				throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
			}
		}

		public static void UpdatePrescription(Prescrire unP)
		{
			Serreurs er = new Serreurs("Erreur sur la mise à jour du médicament.", "ServiceMedicament.UpdatePrescription()");
			String req = "UPDATE prescrire " +
			" SET id_dosage = " + unP.Id_dosage + ", " +
			" id_type_individu = " + unP.Id_type_individu + ", " +
			" posologie = '" + unP.Posologie + "'" +
			" WHERE id_medicament = " + unP.Id_medicament;
			try
			{
				DBInterface.Execute_Transaction(req);
			}
			catch (MonException e)
			{
				throw e;
			}
		}


		public static void InsertPrescription(Prescrire unP)
		{
			Serreurs er = new Serreurs("Erreur sur l'insertion du médicament.", "ServiceMedicament.InsertPrescription()");
			String req = "INSERT INTO prescrire (id_medicament, id_dosage, id_type_individu, posologie) VALUES (" +
				unP.Id_medicament + ", " + unP.Id_dosage + ", " + unP.Id_type_individu + ", '" + unP.Posologie + "')";
			try
			{
				DBInterface.Execute_Transaction(req);
			}
			catch (MonException e)
			{
				throw e;
			}
		}


        /// methode pour supprimer un médicament à l'aide de son identifiant

        public static void DeletePrescription(string id)
        {
            Serreurs er = new Serreurs("Erreur sur la suppression du médicament.", "ServiceMedicament.DeletePrescription()");
            String req = "DELETE FROM prescrire WHERE id_medicament = " + id;
            try
            {
                DBInterface.Execute_Transaction(req);
            }
            catch (MonException e)
            {
                throw e;
            }
        }

    }
}

