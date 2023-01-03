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
        /* //Chauffeurs
        Salarie s21 = new Salarie("Roma", "David", new DateTime(1985, 06, 16), new Adresse("Paris", "Rue de vin"), "romaDav@transco.fr", "0704896824", new DateTime(2006, 01, 05), new Chauffeur(), new List<Salarie>());
        Salarie s20 = new Salarie("Romi", "Marie", new DateTime(1990, 11, 05), new Adresse("Paris", "allée ladouille"), "missRomi@transco.fr", "0696550111", new DateTime(2015, 01, 11), new Chauffeur(), new List<Salarie>());
        Salarie s19 = new Salarie("Rimou", "Maxence", new DateTime(1995, 05, 03), new Adresse("Paris", "Rue de meaux"), "RimouMax@transco.fr", "0701233824", new DateTime(2017, 07, 01), new Chauffeur(), new List<Salarie>());
        Salarie s18 = new Salarie("Romu", "Maluma", new DateTime(1996, 09, 24), new Adresse("Paris", "allée valoo"), "RomuMaloma@transco.fr", "0707770824", new DateTime(2016, 01, 08), new Chauffeur(), new List<Salarie>());
        Salarie s17 = new Salarie("Rome", "Vanille", new DateTime(1975, 07, 21), new Adresse("Paris", "Rue de calouil"), "RomeVan@transco.fr", "0706055689", new DateTime(2000, 01, 07), new Chauffeur(), new List<Salarie>());
        //Chefs equipes
        Salarie s16 = new Salarie("Royal", "Louis", new DateTime(1981, 03, 11), new Adresse("Paris", "Rue de cerceuille"), "RoyalLouis@transco.fr", "07686956423", new DateTime(2013, 01, 01), new ChefEquipe(), new List<Salarie> { s21, s20, s18 });
        Salarie s15 = new Salarie("Prince", "Priclia", new DateTime(1977, 05, 21), new Adresse("Paris", "Rue de aboudabi"), "PrincePricilia@transco.fr", "0702250824", new DateTime(2016, 11, 01),new ChefEquipe(), new List<Salarie> {s19, s17 });
        //Directeur Op
        Salarie s14 = new Salarie("Fetard", "LaBringue", new DateTime(1969, 11, 12), new Adresse("Paris", "Rue de neuilly"), "FetardLaB@transco.fr", "0798653412", new DateTime(1995, 01, 01), new DirecteurDesOps(), new List<Salarie> { s16,s15});
        //Commerciales
        Salarie s13 = new Salarie("Fermi", "Emilie", new DateTime(2001, 01, 25), new Adresse("Paris", "Rue de nouille"), "FermiEmilie@transco.fr", "0712345687", new DateTime(2021, 09, 11), new Commercial(), new List<Salarie>());
        Salarie s12 = new Salarie("Forge", "Yanis", new DateTime(1998, 06, 21), new Adresse("Paris", "Rue de sercol"), "ForgeYanis@transco.fr", "0658698496", new DateTime(2019, 01, 01), new Commercial(), new List<Salarie>());
        //Directrice commerciale
        Salarie s11 = new Salarie("Fiesta", "Manon", new DateTime(1985, 02, 11), new Adresse("Paris", "Rue de valentino"), "FiestaManon@transco.fr", "0626050824", new DateTime(2013, 01, 11), new DirecteurCommercial(), new List<Salarie> { s13, s12 });
        //Contrat/formation
        Salarie s10 = new Salarie("ToutleMonde", "Jean", new DateTime(1980, 04, 10), new Adresse("Paris", "Rue de lamolle"), "ToutleMonde@transco.fr", "0706050824", new DateTime(2010, 01, 08), new Formation(), new List<Salarie>());
        Salarie s9 = new Salarie("Couleur", "Alix", new DateTime(1950, 08, 11), new Adresse("Paris", "Rue de vert"), "Couleur@transco.fr", "0735684124", new DateTime(2016, 11, 01), new Contrat(), new List<Salarie>());
        //DiteurRh
        Salarie s8 = new Salarie("Joyeuse", "Alice", new DateTime(1972, 11, 30), new Adresse("Paris", "Rue de ross"), "Joyeuse@transco.fr", "0706032568", new DateTime(2011, 01, 01), new DirecteurRH(), new List<Salarie> { s10,s9});
        //Comptable
        Salarie s7 = new Salarie("Gautier", "Gautier", new DateTime(1999, 11, 20), new Adresse("Paris", "Rue de neuilly"), "Gautier@transco.fr", "065533824", new DateTime(2021, 11, 02), new Comptable(), new List<Salarie>());
        Salarie s6 = new Salarie("Fournier", "Fabien", new DateTime(1999, 03, 14), new Adresse("Paris", "Rue de amsterdam"), "Fournier@transco.fr", "0685895642", new DateTime(2022, 01, 01), new Comptable(), new List<Salarie>());
        //Direction comptable
        Salarie s5 = new Salarie("Picsou", "Amir",new DateTime(1988,03,11),new Adresse("Paris","Rue de venise"), "Picsou@transco.fr", "0768695424",new DateTime(2012,01,01),new DirectionComptable(),new List<Salarie> { s7,s6});
        //Contreoleur gestion
        Salarie s4 = new Salarie("GrosSous", "Paul", new DateTime(1985, 11, 11), new Adresse("Paris", "Rue de vaul"), "GrosSous@transco.fr", "0768698487", new DateTime(2010, 01, 01), new ControleurDeGestion(), new List<Salarie>());
        //Directeur financier
        Salarie s3 = new Salarie("GripSous", "Rachid", new DateTime(1975, 11, 11), new Adresse("Paris", "Rue de neuilly"), "GripSous@transco.fr", "0735263577", new DateTime(1990, 01, 01), new DirecteurFinancier(), new List<Salarie> {s5,s4});
        //CEO
        Salarie s1 = new Salarie("Dupond","Jérome",new DateTime(1967,11,11),new Adresse("Paris","Rue de neuilly"),"jéromeDupond@transco.fr","0706050824",new DateTime(1990,01,01),new DirecteurGeneral(),new List<Salarie> { s14,s11,s8,s3});

        List<Salarie> lesSalaries = new List<Salarie> { s1, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s15, s16, s17, s18, s19, s20, s21 };
        JsonUtil.sendJsonSalaries(lesSalaries);
        */


        var converter = new PosteConverter();
        List<Salarie> lesSalaries = new List<Salarie>();
        JsonUtil.getJsonSalaries(ref lesSalaries, converter);
        TransconnectControleur controleur = new TransconnectControleur(lesSalaries);

        //TODO: !!!!!!!!!!!IL FAUT AJOUTER DANS TOUT LES EMPLOYÉ QUI ON L'EMPLOYÉ ET PAREILLE POUR SUPRIMER
        foreach (var item in controleur.Salaries.Find(x => x.Nom == "Fetard").Employ.Find(x => x.Nom=="Royal").Employ)
        {
            Console.WriteLine(item.ToString());
        }
        controleur.deleteSalarie("Roma", "David");
        Console.WriteLine("\n" + controleur.showOrgannigramme() + "\n");
        controleur.BuildSalariesTree();
        Console.WriteLine("\n" + controleur.showOrgannigramme() + "\n");

        //TODO: Pour les clients faire le parse de file
        #endregion

        //controleur.BuildSalariesTree();
        //Console.WriteLine(controleur.showOrgannigramme()+"\n");

        //petit test
        /*foreach (var item in controleur.Salaries)
        {
            Console.WriteLine(item.ToString());

        }*/
        /*Salarie s17 = new Salarie("Romeffso", "Vanislle", new DateTime(1965, 07, 21), new Adresse("Paris", "Rue de calouil"), "RomeVan@transco.fr", "0706055689", new DateTime(2000, 01, 07), new Chauffeur(), new List<Salarie>());

        controleur.addSalarie(s17, "Royal", "Louis");
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
