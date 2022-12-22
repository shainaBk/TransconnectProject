using TransconnectProject.Model;
using TransconnectProject.Model.PosteModel;
using TransconnectProject.Util;
using TransconnectProject.Controleur;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ProjectsTests
{
    public class PersonnesTests
    {
        private Personne p1;
        private Salarie p2;
        private Salarie p3;
        private Salarie p4;
        private Salarie p5;
        private Salarie p6;
        private List<Salarie> listSalarie = new List<Salarie>();
        private List<Salarie> listSalarieTest = new List<Salarie>();
        private TransconnectControleur controler;

        private TransconnectControleur controler2;
        private List<Salarie> listSalarie2 = new List<Salarie>();
        private Salarie ceo;
        private Salarie emp1;
        private Salarie emp2;
        private Salarie subEmp3;



        [SetUp]
        public void setup()
        {
            p1 = new Client("Messi", "Lionnel", new DateTime(2001, 03, 11), new Adresse("Buenas Aires", "Los pequenos y pequenas"), "messi@hotmail.com", "0658497123");
            p3 = new Salarie("Bakili", "Shaina", new DateTime(2001, 03, 11), new Adresse("Ermont", "all�e manon des sources"), "shaina3322@hotmail.com", "0768698493", new DateTime(2022, 01, 06), new Chauffeur(), new List<Salarie>());
            p2 = new Salarie("Lapin", "Catarina", new DateTime(2001, 03, 11), new Adresse("Madrid", "la casa de papel"), "Cata@hotmail.com", "07536984025", new DateTime(2022, 01, 06), new ChefEquipe(), new List<Salarie> { p3 });
            p4 = new Salarie("LePain", "Jean Pierre", new DateTime(2001, 03, 11), new Adresse("Paris", "6 rue jean bouins"), "JP@hotmail.com", "0765629493", new DateTime(2022, 01, 06), new Chauffeur(), new List<Salarie>());
            p6 = new Salarie("LePain2", "Jean Pierre2", new DateTime(2001, 03, 11), new Adresse("Paris", "6 rue jean bouins"), "JP@hotmail.com", "0765629493", new DateTime(2022, 01, 06), new Chauffeur(), new List<Salarie>());
            p5 = new Salarie("Vanackor", "Coco", new DateTime(2001, 03, 11), new Adresse("Paris", "6 rue jean bouins"), "JP@hotmail.com", "0765629493", new DateTime(2022, 01, 06), new Chauffeur(), new List<Salarie>());
            listSalarie.Add(p2); listSalarie.Add(p3);
            controler = new TransconnectControleur(listSalarie);


            //Specifique organigrame
            subEmp3 = new Salarie("Bakili", "Shaina", new DateTime(2001, 03, 11), new Adresse("Ermont", "all�e manon des sources"), "shaina3322@hotmail.com", "0768698493", new DateTime(2022, 01, 06), new Chauffeur(), new List<Salarie>());
            emp1 = new Salarie("Lapin", "Catarina", new DateTime(2001, 03, 11), new Adresse("Madrid", "la casa de papel"), "Cata@hotmail.com", "07536984025", new DateTime(2022, 01, 06), new ChefEquipe(), new List<Salarie> { subEmp3 });
            ceo = new Salarie("Messi", "Lionnel", new DateTime(2001, 03, 11), new Adresse("Buenas Aires", "Los pequenos y pequenas"), "messi@hotmail.com", "0658497123", new DateTime(2022, 01, 06),new DirecteurGeneral(), new List<Salarie> { emp1 });
            listSalarie2.Add(ceo);listSalarie2.Add(emp1);listSalarie2.Add(subEmp3);
            controler2 = new TransconnectControleur(listSalarie2);
        }

        [Test]
        public void InitPersonnesTest()
        {
            //Client
            Assert.AreEqual(p1.Prenom, "Lionnel");
            Assert.AreEqual(p1.TelNum, "0658497123");

            //Salarie
            Assert.AreEqual(p3.Dob.ToString("d"), "11/03/2001");
            Assert.AreEqual(p4.Poste.NomPoste, "Chauffeur");

           
        }
        [Test]
        public void ControleurTest()
        {
            #region addSalarie()
            //Before adding p4
            Salarie p2Controler;
            Assert.AreEqual(2,controler.Salaries.Count());
            Assert.AreEqual(1,p2.Employ.Count());

            //After adding p4
            controler.addSalarie(p4);
            controler.addSalarie(p6);
            Assert.AreEqual(4,controler.Salaries.Count());
            p2Controler = controler.Salaries.Find(x => x.Equals(p2));
            Assert.AreEqual(3,p2Controler.Employ.Count());
            #endregion

            #region OrgannigrammePart();
            controler2.BuildSalariesTree();
            Console.WriteLine(controler2.showOrgannigramme());
            #endregion
        }
        [Test]
        public void JsonUtilTest()
        {
            int initaialSize = listSalarie.Count();
            var converter = new PosteConverter();

            //Convert list au format JSON & Write to file
            JsonUtil.sendJsonSalaries(listSalarie);

            //Tests Get from file | Pour liste de salarie
            JsonUtil.getJsonSalaries(ref listSalarieTest, converter);//Windows rep \Users\Sha�na\Esilv\TransconnectProject\TransconnectProject\serializationFiles\Salaries.json
            Assert.AreEqual(listSalarie.Count(), listSalarieTest.Count());
            for (int i = 0; i < listSalarie.Count; i++)
            {
                Assert.AreEqual(listSalarie[i].Nom, listSalarieTest[i].Nom);
                Assert.AreEqual(listSalarie[i].GetType(), listSalarieTest[i].GetType());
            }

            //Test ajout salarié from controler
            int initSizeP2Employes = p2.Employ.Count();
            controler.addSalarie(p4);
            JsonUtil.getJsonSalaries(ref listSalarieTest, converter);
            Assert.AreEqual(initaialSize + 1,listSalarieTest.Count());
            Assert.AreEqual(initSizeP2Employes + 1, 2);

           //Test suprimer salarié from controler
            controler.deleteSalarie("Bakili","Shaina");
            Assert.AreEqual(initaialSize,listSalarie.Count());
        }
        [Test]
        public void nArryTreeTest() {
            SalarieTree tree = new SalarieTree(new SalarieNode(p2, new List<Node<Salarie>> { new SalarieNode(p3), new SalarieNode(p4, new List<Node<Salarie>> { new SalarieNode(p3) }), new SalarieNode(p5, new List<Node<Salarie>> { new SalarieNode(p3, new List<Node<Salarie>> { new SalarieNode(p4)}), new SalarieNode(p2)})}));
            //Console.WriteLine("\n"+tree.ToString());

            
        }
    }
}