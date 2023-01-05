using TransconnectProject.Model;
using TransconnectProject.Util;
using TransconnectProject.Model.PosteModel;
using TransconnectProject.Controleur;
public class main
{
    //TODO: FINIR INIT SALARIES
    public static void Main()
    {
        #region InitZone
        var converter = new PosteConverter();
        List<Salarie> lesSalaries = new List<Salarie>();
        JsonUtil.getJsonSalaries(ref lesSalaries, converter);
        TransconnectControleur controleur = new TransconnectControleur(lesSalaries);
        #endregion

        //TODO: !!!!!!!!!!!IL FAUT AJOUTER DANS TOUT LES EMPLOYÉ QUI ON L'EMPLOYÉ ET PAREILLE POUR SUPRIMER
        /* foreach (var item in controleur.Salaries.Find(x => x.Nom == "Fetard").Employ.Find(x => x.Nom=="Royal").Employ)
         {
             Console.WriteLine(item.ToString());
         }*/
        Salarie s17 = new Salarie("Romeffso", "Vanislle", new DateTime(1965, 07, 21), new Adresse("Paris", "Rue de calouil"), "RomeVan@transco.fr", "0706055689", new DateTime(2000, 01, 07), new Chauffeur(), new List<Salarie>());
        controleur.addSalarie(s17, "Royal", "Louis");
        //controleur.deleteSalarie("Romeffso", "Vanislle");
        Console.WriteLine("\n" + controleur.showOrgannigramme() + "\n");

        //TODO: Pour les clients faire le parse de file
        

        //controleur.BuildSalariesTree();
        //Console.WriteLine(controleur.showOrgannigramme()+"\n");

        //petit test
        /*foreach (var item in controleur.Salaries)
        {
            Console.WriteLine(item.ToString());

        }*/
        /*
        controleur.BuildSalariesTree();

        foreach (var item in controleur.Salaries.Find(x=>x.Nom== "Royal").Employ)
        {
            Console.WriteLine(item.ToString());
        }
        Console.WriteLine("\n"+controleur.showOrgannigramme()+"\n");*/


        /* foreach (var item in controleur.Salaries)
         {
             Console.WriteLine(item.ToString());

         }*/
    }
}
