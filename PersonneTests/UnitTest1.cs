using TransconnectProject.Model;
using TransconnectProject.Model.PosteModel;
using TransconnectProject.Util;

namespace PersonneTests
{
    [TestClass]
    public class UnitTest1
    {
        //Before
        public UnitTest1() {
            Personne p1 = new Client("Bakili", "Shaïna", new DateTime(2001, 03, 11), new Adresse("Ermont", "allée manon des sources"), "shaina3322@hotmail.com", "0768698493");
            Salarie p3 = new Salarie("Bakili", "Shaïna", new DateTime(2001, 03, 11), new Adresse("Ermont", "allée manon des sources"), "shaina3322@hotmail.com", "0768698493", new DateTime(2022, 01, 06), new Chauffeur(), new List<Salarie>());
            Salarie p2 = new Salarie("Bakili", "Shaïna2", new DateTime(2001, 03, 11), new Adresse("Ermont", "allée manon des sources"), "shaina3322@hotmail.com", "0768698493", new DateTime(2022, 01, 06), new ChefEquipe(), new List<Salarie> { p3 });
            Salarie p4 = new Salarie("Bakili", "Shaïna3", new DateTime(2001, 03, 11), new Adresse("Ermont", "allée manon des sources"), "shaina3322@hotmail.com", "0768698493", new DateTime(2022, 01, 06), new Chauffeur(), new List<Salarie>());

        }
        [TestMethod]
        public void TestInitPersonnes()
        {

        }
    }
}