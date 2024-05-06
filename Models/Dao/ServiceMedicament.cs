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
                String req = "SELECT medicament.id_medicament, medicament.id_famille, medicament.depot_legal,medicament.nom_commercial, medicament.effets, medicament.contre_indication, medicament.prix_echantillon, famille.lib_famille, dosage.qte_dosage, dosage.unite_dosage, prescrire.posologie, type_individu.lib_type_individu "
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

        /*public static DataTable GetMedicamentById(int id)
        {
            DataTable mesMedicaments;
            Serreurs er = new Serreurs("Erreur sur la lecture des médicaments.", "ServiceMedicament.GetMedicamentById()");
            try
            {
                String req = "SELECT medicament.id_medicament, medicament.id_famille, medicament.depot_legal, " +
                                    "medicament.nom_commercial, medicament.effets, medicament.contre_indication, " +
                                    "medicament.prix_echantillon, famille.lib_famille " +
                                    "FROM medicament " +
                                    "JOIN famille ON medicament.id_famille = famille.id_famille " +
                                    "WHERE medicament.id_medicament = " + id;

                mesMedicaments = DBInterface.Lecture(req, er);
                return mesMedicaments;
            }
            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }*/
    }
}

