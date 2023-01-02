using TransconnectProject.Model;
using TransconnectProject.Util;
using TransconnectProject.Model.PosteModel;
using TransconnectProject.Controleur;
public class main
{
    public static void Main()
    {
        #region InitZone
        //Chauffeurs
        Salarie s21 = new Salarie("Roma", "David", new DateTime(1985, 16, 06), new Adresse("Paris", "Rue de vin"), "romaDav@transco.fr", "0704896824", new DateTime(2006, 01, 05), new Chauffeur(), new List<Salarie>());
        Salarie s20 = new Salarie("Romi", "Marie", new DateTime(1990, 11, 05), new Adresse("Paris", "allée ladouille"), "missRomi@transco.fr", "0696550111", new DateTime(2015, 01, 11), new Chauffeur(), new List<Salarie>());
        Salarie s19 = new Salarie("Rimou", "Maxence", new DateTime(1995, 05, 03), new Adresse("Paris", "Rue de meaux"), "RimouMax@transco.fr", "0701233824", new DateTime(2017, 07, 01), new Chauffeur(), new List<Salarie>());
        Salarie s18 = new Salarie("Romu", "Maluma", new DateTime(1996, 24, 09), new Adresse("Paris", "allée valoo"), "RomuMaloma@transco.fr", "0707770824", new DateTime(2016, 01, 08), new Chauffeur(), new List<Salarie>());
        Salarie s17 = new Salarie("Rome", "Vanille", new DateTime(1975, 21, 07), new Adresse("Paris", "Rue de calouil"), "RomeVan@transco.fr", "0706055689", new DateTime(2000, 01, 07), new Chauffeur(), new List<Salarie>());
        //Chefs equipes
        Salarie s16 = new Salarie("Royal", "Louis", new DateTime(1981, 03, 11), new Adresse("Paris", "Rue de cerceuille"), "RoyalLouis@transco.fr", "07686956423", new DateTime(2013, 01, 01), new ChefEquipe(), new List<Salarie> { s21, s20, s18 });
        Salarie s15 = new Salarie("Prince", "Priclia", new DateTime(1977, 21, 05), new Adresse("Paris", "Rue de aboudabi"), "PrincePricilia@transco.fr", "0702250824", new DateTime(2016, 11, 01),new ChefEquipe(), new List<Salarie> {s19, s17 });
        //Directeur Op
        Salarie s14 = new Salarie("Fetard", "LaBringue", new DateTime(1969, 11, 12), new Adresse("Paris", "Rue de neuilly"), "FetardLaB@transco.fr", "0798653412", new DateTime(1995, 01, 01), new DirecteurDesOps(), new List<Salarie> { s16,s15});
        //Commerciales
        Salarie s13 = new Salarie("Fermi", "Emilie", new DateTime(2001, 25, 01), new Adresse("Paris", "Rue de nouille"), "FermiEmilie@transco.fr", "0712345687", new DateTime(2021, 09, 11), new Commercial(), new List<Salarie>());
        Salarie s12 = new Salarie("Forge", "Yanis", new DateTime(1998, 21, 06), new Adresse("Paris", "Rue de sercol"), "ForgeYanis@transco.fr", "0658698496", new DateTime(2019, 01, 01), new Commercial(), new List<Salarie>());
        //Directrice commerciale
        Salarie s11 = new Salarie("Fiesta", "Manon", new DateTime(1985, 02, 11), new Adresse("Paris", "Rue de valentino"), "FiestaManon@transco.fr", "0626050824", new DateTime(2013, 01, 11), new DirecteurCommercial(), new List<Salarie> { s13, s12 });
        //Contrat/formation
        Salarie s10 = new Salarie("Dupond", "Jérome", new DateTime(1950, 11, 11), new Adresse("Paris", "Rue de neuilly"), "jéromeDupond@transco.fr", "0706050824", new DateTime(1990, 01, 01), new Poste("Directeur general", null, 15000), new List<Salarie>());
        Salarie s9 = new Salarie("Dupond", "Jérome", new DateTime(1950, 11, 11), new Adresse("Paris", "Rue de neuilly"), "jéromeDupond@transco.fr", "0706050824", new DateTime(1990, 01, 01), new Poste("Directeur general", null, 15000), new List<Salarie>());
        //DiteurRh
        Salarie s8 = new Salarie("Dupond", "Jérome", new DateTime(1950, 11, 11), new Adresse("Paris", "Rue de neuilly"), "jéromeDupond@transco.fr", "0706050824", new DateTime(1990, 01, 01), new Poste("Directeur general", null, 15000), new List<Salarie>());
        //Comptable
        Salarie s7 = new Salarie("Dupond", "Jérome", new DateTime(1950, 11, 11), new Adresse("Paris", "Rue de neuilly"), "jéromeDupond@transco.fr", "0706050824", new DateTime(1990, 01, 01), new Poste("Directeur general", null, 15000), new List<Salarie>());
        Salarie s6 = new Salarie("Dupond", "Jérome", new DateTime(1950, 11, 11), new Adresse("Paris", "Rue de neuilly"), "jéromeDupond@transco.fr", "0706050824", new DateTime(1990, 01, 01), new Poste("Directeur general", null, 15000), new List<Salarie>());
        //Direction comptable
        Salarie s5 = new Salarie("Dupond","Jérome",new DateTime(1950,11,11),new Adresse("Paris","Rue de neuilly"),"jéromeDupond@transco.fr","0706050824",new DateTime(1990,01,01),new Poste("Directeur general", null, 15000),new List<Salarie>());
        //Contreoleur gestion
        Salarie s4 = new Salarie("Dupond", "Jérome", new DateTime(1950, 11, 11), new Adresse("Paris", "Rue de neuilly"), "jéromeDupond@transco.fr", "0706050824", new DateTime(1990, 01, 01), new Poste("Directeur general", null, 15000), new List<Salarie>());
        //Directeur financier
        Salarie s3 = new Salarie("Dupond", "Jérome", new DateTime(1950, 11, 11), new Adresse("Paris", "Rue de neuilly"), "jéromeDupond@transco.fr", "0706050824", new DateTime(1990, 01, 01), new Poste("Directeur general", null, 15000), new List<Salarie>());
        Salarie s2 = new Salarie("Dupond", "Jérome", new DateTime(1950, 11, 11), new Adresse("Paris", "Rue de neuilly"), "jéromeDupond@transco.fr", "0706050824", new DateTime(1990, 01, 01), new Poste("Directeur general", null, 15000), new List<Salarie>());
        //CEO
        Salarie s1 = new Salarie("Dupond","Jérome",new DateTime(1967,11,11),new Adresse("Paris","Rue de neuilly"),"jéromeDupond@transco.fr","0706050824",new DateTime(1990,01,01),new DirecteurGeneral(),new List<Salarie> { s14,s11,s8,s3});
        TransconnectControleur controleur;
        #endregion
    }
}
