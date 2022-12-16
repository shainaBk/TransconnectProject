using TransconnectProject.Model;
using TransconnectProject.Util;
using TransconnectProject.Model.PosteModel;
public class main
{
    public static void Main()
    {
        //TESTS INIT Personne
        Personne p1 = new Client("Bakili", "Shaïna", new DateTime(2001, 03, 11), new Adresse("Ermont", "allée manon des sources"), "shaina3322@hotmail.com", "0768698493");
        ;//attention retirer l'heure
        Console.WriteLine(p1.TelNum);

        /***************/
        Salarie p3= new Salarie("Bakili", "Shaïna", new DateTime(2001, 03, 11), new Adresse("Ermont", "allée manon des sources"), "shaina3322@hotmail.com", "0768698493", new DateTime(2022, 01, 06), new Chauffeur(), new List<Salarie>());
        Salarie p2 = new Salarie("Bakili", "Shaïna2", new DateTime(2001, 03, 11), new Adresse("Ermont", "allée manon des sources"), "shaina3322@hotmail.com", "0768698493", new DateTime(2022, 01, 06), new ChefEquipe(), new List<Salarie>{p3});
        List<Salarie> list = new List<Salarie>() {p2,p3};
        foreach (var item in p2.Employ)
        {
            Console.WriteLine(item.Prenom+" "+item.Poste);
        }

        TransconnectControleur t = new TransconnectControleur(list);

        Salarie p4 = new Salarie("Bakili", "Shaïna3", new DateTime(2001, 03, 11), new Adresse("Ermont", "allée manon des sources"), "shaina3322@hotmail.com", "0768698493", new DateTime(2022, 01, 06), new Chauffeur(), new List<Salarie>());
        Console.WriteLine("TYheree => "+p4.Poste.getNumHierarchique());
        //TESTS ADD EMPLOYE
        t.addSalarie(p4);
        foreach (var item in p2.Employ)
        {
            Console.WriteLine(item.Prenom + " " + item.Poste);
        }
    }
}

public class TransconnectControleur
{
    private List<Salarie> salaries;

    public TransconnectControleur(List<Salarie> salaries)
    {
        this.salaries = salaries;
    }

    public void addSalarie(Salarie s)
    {
        List<Salarie> Dep = salaries.FindAll((x) =>  x.Poste.Departement.Nom.Equals(s.Poste.Departement.Nom));
        /** ADD Condition si existe pas; Peut etre exception ?**/
        //Temporary
        if(Dep.Count == 0)
            Console.WriteLine("none");
        else
        {
            foreach (Salarie item in Dep)
            {
                if (item.Poste.getNumHierarchique() < s.Poste.getNumHierarchique())
                    item.Employ.Add(s);
            }
        }
        salaries.Add(s);
        //Log("Employé ajouté !!)
    }
}