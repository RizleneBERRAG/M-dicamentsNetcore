using System.Data;
using Médicaments.Models.MesExceptions;
using Médicaments.Models.Persistance;

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
                String req = "select * from medicament";
                mesMedicaments = DBInterface.Lecture(req, er);
                return mesMedicaments;
            }
            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }

        }
    }
}
