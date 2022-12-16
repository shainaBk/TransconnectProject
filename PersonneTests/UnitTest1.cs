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
            Personne p1 = new Client("Bakili", "Sha�na", new DateTime(2001, 03, 11), new Adresse("Ermont", "all�e manon des sources"), "shaina3322@hotmail.com", "0768698493");
            Salarie p3 = new Salarie("Bakili", "Sha�na", new DateTime(2001, 03, 11), new Adresse("Ermont", "all�e manon des sources"), "shaina3322@hotmail.com", "0768698493", new DateTime(2022, 01, 06), new Chauffeur(), new List<Salarie>());
            Salarie p2 = new Salarie("Bakili", "Sha�na2", new DateTime(2001, 03, 11), new Adresse("Ermont", "all�e manon des sources"), "shaina3322@hotmail.com", "0768698493", new DateTime(2022, 01, 06), new ChefEquipe(), new List<Salarie> { p3 });
            Salarie p4 = new Salarie("Bakili", "Sha�na3", new DateTime(2001, 03, 11), new Adresse("Ermont", "all�e manon des sources"), "shaina3322@hotmail.com", "0768698493", new DateTime(2022, 01, 06), new Chauffeur(), new List<Salarie>());

        }
        [TestMethod]
        public void TestInitPersonnes()
        {

        }
    }
}