using TransconnectProject.Model;
using TransconnectProject.Model.PosteModel;
using TransconnectProject.Util;

namespace ProjectsTests
{
    public class PersonnesTests
    {
        private Personne p1;
        private Salarie p2;
        private Salarie p3;
        private Salarie p4;
        private List<Salarie> listSalarie = new List<Salarie>();
        //Before
        [SetUp]
        public void setup()
        {
            p1 = new Client("Bakili", "Shaïna", new DateTime(2001, 03, 11), new Adresse("Ermont", "allée manon des sources"), "shaina3322@hotmail.com", "0768698493");
            p3 = new Salarie("Bakili", "Shaïna", new DateTime(2001, 03, 11), new Adresse("Ermont", "allée manon des sources"), "shaina3322@hotmail.com", "0768698493", new DateTime(2022, 01, 06), new Chauffeur(), new List<Salarie>());
            p2 = new Salarie("Bakili", "Shaïna2", new DateTime(2001, 03, 11), new Adresse("Ermont", "allée manon des sources"), "shaina3322@hotmail.com", "0768698493", new DateTime(2022, 01, 06), new ChefEquipe(), new List<Salarie> { p3 });
            p4 = new Salarie("Bakili", "Shaïna3", new DateTime(2001, 03, 11), new Adresse("Ermont", "allée manon des sources"), "shaina3322@hotmail.com", "0768698493", new DateTime(2022, 01, 06), new Chauffeur(), new List<Salarie>());
            listSalarie.Add(p2); listSalarie.Add(p3);
            TransconnectControleur t = new TransconnectControleur(listSalarie);
        }

        [Test]
        public void TestInitPersonnes()
        {
            //Client
            Assert.AreEqual(p1.Prenom, "Shaïna");
            Assert.AreEqual(p1.TelNum, "0768698493");

            //Salarie
            Assert.AreEqual(p1.Dob.ToString("d"), "11/03/2001");
            Assert.AreEqual(p4.Poste.Nom, "Chauffeur");

        }

        [Test]
        public void ControleurTest()
        {
            #region addSalarie()
            //Before adding p4
            Assert.AreEqual(listSalarie.Count(),2);

            //After adding p4
            listSalarie.Add(p4);
            Assert.AreEqual(listSalarie.Count(), 3);
            #endregion

        }
    }
}