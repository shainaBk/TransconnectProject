using TransconnectProject.Model;
using TransconnectProject.Util;
using TransconnectProject.Model.PosteModel;
using TransconnectProject.Controleur;
using TransconnectProject.Controleur.CritereClients;
using TransconnectProject.Model.ProduitModel;
using TransconnectProject.Model.VehiculeModel;
using TransconnectProject.Model.CommandeModel;
using TransconnectProject.Model.DepartementModel;

public class main
{
    public static void pressToContinue() {
        Console.WriteLine("\nAppuyer entrer pour continuer...");
        Console.ReadLine();
        Console.Clear();
    }
    public static void Main()
    {
        #region InitZone
        var converter = new PosteConverter();
        List<Salarie> lesSalaries = new List<Salarie>();
        List<Client> lesClients = new List<Client>();
        List<Commande> lesCommandes = new List<Commande>();
        List<Produit> lesProduits = new List<Produit> { new Produit("Chocolat", 5.5), new Produit("Huile", 2.5), new Produit("Vin", 10.6), new Produit("Cuire", 5.0), new Produit("Metal", 25.0), new Produit("Argent", 22.41), new Produit("Platine", 1024.0), new Produit("Or", 1754.1) };
        List<Vehicule> lesVehicules = new List<Vehicule> { new Voiture(6),new Camionette("Transport chocolat"), new Camion(1500, "Metal", "Camion benne"), new Camion(1500, "Cuire", "Camion benne"), new Camion(1500, "Platine", "Camion benne"), new Camion(2000, "or", "Camion benne"), new Camion(1000, "Huile", "camion-citerne"), new Camion(1000, "Vin", "camion-citerne") };
        JsonUtil.getJsonSalaries(ref lesSalaries, converter);
        JsonUtil.getJsonClients(ref lesClients);
        /********** Chargement des commandes ************/
        foreach (var item in lesClients)
        {
            foreach (var item2 in item.CommandesClient)
            {
                if(item.CommandesClient.Count()>0)
                    lesCommandes.Add(item2);
            }
        }
        /**********************************************/
        /********** Chargement des commandes chauffeurs ************/
        List<Salarie> chauf = lesSalaries.FindAll(x => x.Poste.NomPoste == "Chauffeur");
        if (lesCommandes.Count() > 0)
        {
            foreach (var item in chauf)
            {
                foreach (var item2 in lesCommandes)
                {
                    if (item.Nom.Equals(item2.ChauffeurCom.Nom) && item.Prenom.Equals(item2.ChauffeurCom.Prenom))
                        ((Chauffeur)item.Poste).ListeDeCommandes.Add(item2);
                }
            }
        }
        /**********************************************/
        TransconnectControleur controleur = new TransconnectControleur(lesSalaries,lesClients,lesProduits,lesVehicules,lesCommandes);
        controleur.BuildSalariesTree();
        #endregion

        //TODO: !!!!!!!!!!!IL FAUT AJOUTER DANS TOUT LES EMPLOYÉ QUI ON L'EMPLOYÉ ET PAREILLE POUR SUPRIMER
        /* foreach (var item in controleur.Salaries.Find(x => x.Nom == "Fetard").Employ.Find(x => x.Nom=="Royal").Employ)
         {
             Console.WriteLine(item.ToString());
         }*/
        /*Salarie s17 = new Salarie("Romeffso", "Vanislle", new DateTime(1965, 07, 21), new Adresse("Paris", "Rue de calouil"), "RomeVan@transco.fr", "0706055689", new DateTime(2000, 01, 07), new Chauffeur(), new List<Salarie>());
        controleur.addSalarie(s17, "Royal", "Louis");*/
        //controleur.deleteSalarie("Romeffso", "Vanislle");
        //Console.WriteLine("\n" + controleur.showOrgannigramme() + "\n");

        #region MenuZone
        string SFirstINPUT;
        int INPUT;
        bool FIRSTMENU = true;

        while (FIRSTMENU)
        {
            Console.WriteLine("----------------------------- BIENVENUE SUR TRANSCONNECT SOFTWARE ----------------------------- \n");
            Console.WriteLine("Choisissez un numero pour parcourir:\n\n1. Gestionnaite de clients\n2. Gestionnaire de salaries\n3. Gestionnaire de commandes\n4. Gestionnaire de produits\n5. Gestionnaire de vehicules\nTOUT AUTRE CHIFFRE => Exit\n");
            Console.Write("Votre entrez: ");
            SFirstINPUT = Console.ReadLine();

            Console.Clear();

            while (!int.TryParse(SFirstINPUT, out INPUT))
            {
                Console.WriteLine("Sorry, nous n'avons pas compris votre saisie...\n");
                Console.WriteLine("Choisissez un numero pour parcourir:\n\n1. Gestionnaite de clients\n2. Gestionnaire de salaries\n3. Gestionnaire de commandes\n4. Gestionnaire de produits\n5. Gestionnaire de vehicules\nTOUT AUTRE CHIFFRE => Exit\n");
                Console.Write("Votre entrez: ");
                SFirstINPUT = Console.ReadLine();
                Console.Clear();
            }

            //First switch
            switch (INPUT)
            {
                case 1:
                    #region GESTIONNAIRE CLIENT 
                    bool CLIENTMENU = true;
                    while (CLIENTMENU)
                    {
                        Console.WriteLine("-------------------------- GESTIONNAIRE DE CLIENTS --------------------------\n");
                        Console.WriteLine("Que voulez vous faire ?\n\n1. Afficher liste client \n2. Ajouter un client \n3. Modifier un client \n4. Supprimer un client\n5. Afficher moyenne achat des comptes clients\n6. afficher liste des commandes par client \nTOUT AUTRE CHIFFRE => Menu principal");
                        Console.Write("\nVotre saisie: ");
                        String ClientINPUT = Console.ReadLine();

                        Console.Clear();
                        while (!int.TryParse(ClientINPUT, out INPUT))
                        {
                            Console.WriteLine("Sorry, nous n'avons pas compris votre saisie...\n");
                            Console.WriteLine("Que voulez vous faire ?\n\n1. Afficher liste client \n2. Ajouter un client \n3. Modifier un client \n4. Supprimer un client\n5. Afficher moyenne achat des comptes clients\n6. afficher liste des commandes par client \nTOUT AUTRE CHIFFRE => Menu principal");
                            Console.Write("\nVotre saisie: ");
                            ClientINPUT = Console.ReadLine();
                            Console.Clear();
                        }
                        #region CLIENT SWITCH
                        switch (INPUT)
                        {
                            #region Liste clients
                            case 1:
                                string critere = null;

                                Console.WriteLine("\nChoix des criteres, ATTENTION, si vous n'entrez pas une des lettres proposees, aucun critere ne sera pris en compte.");

                                for (int i = 0; i < 3; i++)
                                {
                                    Console.WriteLine("\nAjouter votre critere numero " + (i + 1) + ": \na. Ordre alphabetique\nb. Ordre ville\nc. Ordre achat cumulé\nTOUT AUTRE CHOIX => Uniquement en normal");
                                    Console.Write("\nVotre saisie: ");
                                    string current = Console.ReadLine();
                                    critere += current;

                                    if (current != "a" && current != "b" && current != "c")
                                    {
                                        Console.WriteLine("\nVotre saisie est incorrecte, nous allons donc afficher les client en mode \"Normal\"");
                                        critere = null;
                                        pressToContinue();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nEntrez \"STOP\" pour ne plus saisir de critere, sinon appuyez sur entrer: ");
                                        if (Console.ReadLine().Equals("STOP", StringComparison.OrdinalIgnoreCase))
                                            break;
                                    }
                                }
                                Console.Clear();
                                switch (critere)
                                {
                                    case "a":
                                        controleur.showClients(false, new List<TransconnectProject.Controleur.CritereClients.ICritere> { new OrdreAlphaCritere() });
                                        pressToContinue();
                                        break;
                                    case "b":
                                        controleur.showClients(false, new List<TransconnectProject.Controleur.CritereClients.ICritere> { new OrdreVilleCritere() });
                                        pressToContinue();
                                        break;
                                    case "c":
                                        controleur.showClients(false, new List<TransconnectProject.Controleur.CritereClients.ICritere> { new OrdreMontantCumule() });
                                        pressToContinue();
                                        break;
                                    case "ab":
                                    case "ba":
                                        controleur.showClients(false, new List<TransconnectProject.Controleur.CritereClients.ICritere> { new OrdreAlphaCritere(), new OrdreVilleCritere() });
                                        pressToContinue();
                                        break;
                                    case "ac":
                                    case "ca":
                                        controleur.showClients(false, new List<TransconnectProject.Controleur.CritereClients.ICritere> { new OrdreAlphaCritere(), new OrdreMontantCumule() });
                                        pressToContinue();
                                        break;
                                    case "bc":
                                    case "cb":
                                        controleur.showClients(false, new List<TransconnectProject.Controleur.CritereClients.ICritere> { new OrdreMontantCumule(), new OrdreVilleCritere() });
                                        pressToContinue();
                                        break;
                                    case "abc":
                                    case "acb":
                                    case "bac":
                                    case "bca":
                                    case "cab":
                                    case "cba":
                                        controleur.showClients(false, new List<TransconnectProject.Controleur.CritereClients.ICritere> { new OrdreAlphaCritere(), new OrdreVilleCritere(), new OrdreMontantCumule() });
                                        pressToContinue();
                                        break;
                                    default:
                                        controleur.showClients();
                                        pressToContinue();
                                        break;
                                }
                                pressToContinue();
                                continue;
                            #endregion
                            #region Ajout client
                            case 2:
                                Console.WriteLine("Ajout d'un client\n");
                                Client newClient = Client.createClient();
                                controleur.addClient(newClient);
                                pressToContinue();
                                continue;
                            #endregion
                            #region Update client
                            case 3:
                                int INPUTclient;
                                Console.WriteLine("- Modification d'un client -\n");
                                Console.WriteLine("Veuillez entrer le numero du client a modifier: \n");
                                for (int i = 0; i < controleur.Clients.Count(); i++)
                                {
                                    Console.WriteLine((i + 1) + ". " + controleur.Clients[i].ToString());
                                }
                                Console.Write("Votre saisie: ");
                                string numClientINPUT = Console.ReadLine();
                                do
                                {
                                    while (!int.TryParse(numClientINPUT, out INPUTclient))
                                    {
                                        Console.WriteLine("Sorry, nous n'avons pas compris votre saisie...\n");
                                        Console.WriteLine("Veuillez entrer le numero du client a modifier: \n");
                                        for (int i = 0; i < controleur.ListeDesProduits.Count(); i++)
                                        {
                                            Console.WriteLine((i + 1) + ". " + controleur.ListeDesProduits[i].ToString());
                                        }
                                        Console.Write("Votre saisie: ");
                                        numClientINPUT = Console.ReadLine();
                                        Console.Clear();
                                    }
                                } while (INPUTclient < 1 || INPUTclient > controleur.ListeDesProduits.Count());
                                Client p = controleur.Clients[INPUTclient - 1];
                                controleur.updateClient(p);
                                pressToContinue();
                                continue;
                            #endregion
                            #region Supprimation client
                            case 4:
                                Console.WriteLine("Supprimation d'un client");
                                Console.Write("\nveuillez saisir le nom du client: ");
                                string nom = Console.ReadLine();
                                Console.Write("\nveuillez saisir le prenom du client: ");
                                string prenom = Console.ReadLine();
                                controleur.deleteClient(nom, prenom);
                                pressToContinue();
                                continue;
                            #endregion
                            //MODULE STATISTIQUE
                            #region Average commande client
                            case 5:
                                Console.WriteLine("- Fonctionnalioté Moyenne achat par client -\n");
                                controleur.showAverageAchatCompteClient();
                                pressToContinue();
                                break;
                            #endregion
                            //MODULE STATISTIQUE
                            #region Liste commandes par client
                            case 6:
                                Console.WriteLine("- Fonctionnalioté liste commandes client -\n");
                                controleur.showClientsListCommandes();
                                pressToContinue();
                                break;
                            #endregion
                            default:
                                CLIENTMENU = false;
                                break;
                        }
                        #endregion
                    }
                    continue;
                #endregion
                case 2:
                    #region GESTIONNAIRE SALARIES
                    //TOKNOW: PROBLEME AJOUT SALARIE WITH SOUS BOSS
                    bool SALARIESMENU = true;
                    while (SALARIESMENU)
                    {
                        Console.WriteLine("-------------------------- GESTIONNAIRE DE SALARIES --------------------------");
                        Console.WriteLine("Que voulez vous faire ?\n\n1. Afficher l'organigramme \n2. Embaucher un salarie \n3. Licencier un salarie\n4. Afficher nombre de livraison par chauffeur\nTOUT AUTRE CHIFFRE => Menu principal \n");
                        Console.Write("\nVotre saisie: ");
                        String SalarieINPUT = Console.ReadLine();
                        Console.Clear();
                        while (!int.TryParse(SalarieINPUT, out INPUT))
                        {
                            Console.WriteLine("Sorry, nous n'avons pas compris votre saisie...\n");
                            Console.WriteLine("Que voulez vous faire ?\n\n1. Afficher l'organigramme \n2. Embaucher un salarie \n3. Licencier un salarie\n4. Afficher nombre de livraison par chauffeur\nTOUT AUTRE CHIFFRE => Menu principal \n");
                            SalarieINPUT = Console.ReadLine();
                            Console.Clear();
                        }
                        //Switch part
                        switch (INPUT)
                        {
                            case 1:
                                Console.WriteLine("- Affichage de l'organigramme -\n");
                                controleur.BuildSalariesTree();
                                Console.WriteLine(controleur.showOrgannigramme());
                                pressToContinue();
                                continue;
                            case 2:
                                Salarie s = Salarie.createSalarie();
                                string name = null;
                                string lastname = null;
                                if (s.Poste.NomPoste == "Chauffeur")
                                {
                                    List<Salarie> lt = controleur.Salaries.FindAll(x =>x.Poste.NomPoste!= "Directeur general" && x.Poste.Departement.NomDep == "Département des operations" && x.Poste.Departement.getNumHierarchique(x.Poste.NomPoste) == 2);
                                    bool ok = false;
                                    int saisie;
                                    do
                                    {
                                        int index = 1;
                                        Console.WriteLine("\nVeuillez saisir le numero du responsable: ");
                                        foreach (var item in lt)
                                        {
                                            Console.WriteLine(index + ". " + item.Nom + " " + item.Prenom);
                                            index++;
                                        }
                                        Console.Write("\nVotre saisie: ");
                                        ok = int.TryParse(Console.ReadLine(),out saisie);

                                    } while (ok==false || saisie <0 || saisie > lt.Count());
                                    lastname = lt[saisie - 1].Nom;
                                    name = lt[saisie - 1].Prenom;
                                }
                                else if (s.Poste.NomPoste == "Comptable")
                                {
                                   Salarie lt = controleur.Salaries.Find(x => x.Poste.NomPoste == "Direction comptable");
                                    lastname = lt.Nom;
                                    name = lt.Prenom;
                                }
                                controleur.addSalarie(s,lastname,name);
                                pressToContinue();
                                continue;
                            case 3:
                                Console.WriteLine("- Fonctionnalite suppression de salarie -\n");
                                Console.Write("\nVeuillez saisir le nom: ");
                                string nom = Console.ReadLine();
                                Console.Write("\nVeuillez saisir le prenom: ");
                                string prenom = Console.ReadLine();
                                while(controleur.Salaries.Find(x=>x.Nom == nom && x.Prenom == prenom) == null)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Erreur, impossible de trouver le salarie.....");
                                    Console.Write("\nVeuillez saisir le nom: ");
                                    nom = Console.ReadLine();
                                    Console.Write("\nVeuillez saisir le prenom: ");
                                    prenom = Console.ReadLine();
                                }
                                Console.Clear();
                                controleur.deleteSalarie(nom, prenom);
                                pressToContinue();
                                continue;
                            case 4:
                                Console.WriteLine("- Affichage nombre livraison par chauffeur -\n");
                                controleur.showChauffeurCommandesNumber();
                                pressToContinue();
                                continue;
                            default:
                                SALARIESMENU = false;
                                break;
                        }
                        Console.Clear();
                    }
                    continue;
                    #endregion
                case 3:
                    #region GESTIONNAIRE COMMANDES
                    bool COMMANDEMENU = true;
                    while (COMMANDEMENU)
                    {
                        Console.WriteLine("-------------------------- GESTIONNAIRE DE COMMANDES --------------------------");
                        Console.WriteLine("Que voulez vous faire ?\n\n1. Effectuer une commande \n2. Modifier une commande \n3. Afficher moyenne des prix des commandes\n4. Afficher les commandes\nTOUT AUTRE CHIFFRE => Menu principal \n");
                        Console.Write("Votre saisie: ");
                        String CommandeINPUT = Console.ReadLine();
                        Console.Clear();
                        while (!int.TryParse(CommandeINPUT, out INPUT))
                        {
                            Console.WriteLine("Sorry, nous n'avons pas compris votre saisie...\n");
                            Console.WriteLine("Que voulez vous faire ?\n\n1. Effectuer une commande \n2. Modifier une commande \n3. Afficher moyenne des prix des commandes\n4. Afficher toutes les commandes\nTOUT AUTRE CHIFFRE => Menu principal \n");
                            Console.Write("Votre saisie: ");
                            CommandeINPUT = Console.ReadLine();
                            Console.Clear();
                        }
                        //Switch part
                        #region COMMANDE SWITCH
                        switch (INPUT)
                        {
                            case 1:
                                int INPUTproduit;
                                int INPUTchauffeur;
                                Console.WriteLine("- Fonctionnalite Effectuer une commande -\n");
                                #region  PART CHOIX PRODUIT
                                Console.WriteLine("Veuillez entrer le numero de votre produit: \n");
                                for (int i = 0; i < controleur.ListeDesProduits.Count(); i++)
                                {
                                    Console.WriteLine((i+1)+". "+ controleur.ListeDesProduits[i].ToString());
                                }
                                Console.Write("Votre saisie: ");
                                string numProduitINPUT = Console.ReadLine();
                                do
                                {
                                    while (!int.TryParse(numProduitINPUT, out INPUTproduit))
                                    {
                                        Console.WriteLine("Sorry, nous n'avons pas compris votre saisie...\n");
                                        Console.WriteLine("Veuillez entrer le numero de votre produit: \n");
                                        for (int i = 0; i < controleur.ListeDesProduits.Count(); i++)
                                        {
                                            Console.WriteLine((i+1) + ". " + controleur.ListeDesProduits[i].ToString());
                                        }
                                        numProduitINPUT = Console.ReadLine();
                                        Console.Clear();
                                    }
                                } while (INPUTproduit < 1 || INPUTproduit > controleur.ListeDesProduits.Count());
                                Produit p = controleur.ListeDesProduits[INPUTproduit - 1];
                                #endregion
                                #region trivial info PART
                                Console.Clear();
                                Console.Write("\nVeuillez saisir la quantite demander pour le produit (en Kg): ");
                                int quantité = int.Parse(Console.ReadLine());
                                Console.Write("\nVeuillez saisir le nom du client: ");
                                string nom = Console.ReadLine();
                                Console.Write("\nVeuillez saisir le Prenom du client: ");
                                string prenom = Console.ReadLine();
                                Console.Write("\nVeuillez saisir la ville de depart: ");
                                string villeFrom = Console.ReadLine();
                                Console.Write("\nVeuillez saisir la date de livraison (yyyy/mm/dd): ");
                                string date = Console.ReadLine();
                                #endregion
                                #region PART CHOIX CHAUFFEUR
                                Console.Clear();
                                Console.WriteLine("Veuillez entrer le numero du chauffeur a affiler: \n");
                                List<Salarie> listeCh = controleur.Salaries.FindAll(x => x.Poste.NomPoste == "Chauffeur");
                                for (int i = 0; i < listeCh.Count(); i++)
                                {
                                    Console.WriteLine((i+1) + ". " +listeCh[i].ToString());
                                }
                                Console.Write("\nVotre saisie: ");
                                string numChauffeurINPUT = Console.ReadLine();
                                do
                                {
                                    while (!int.TryParse(numChauffeurINPUT, out INPUTchauffeur))
                                    {
                                        Console.WriteLine("Sorry, nous n'avons pas compris votre saisie...\n");
                                        Console.WriteLine("Veuillez entrer le numero du chauffeur a affiler: \n");
                                        for (int i = 0; i < listeCh.Count(); i++)
                                        {
                                            Console.WriteLine((i+1) + ". " + listeCh[i].ToString());
                                        }
                                        Console.Write("\nVotre saisie: ");
                                        numChauffeurINPUT = Console.ReadLine();
                                        Console.Clear();
                                    }

                                } while (INPUTchauffeur < 1 || INPUTchauffeur > listeCh.Count());
                                Salarie chauffeur = listeCh[INPUTchauffeur - 1];
                                Console.Clear();
                                #endregion
                                #region PART CHOIX VOITURE
                                string numVehiculeINPUT;
                                int INPUTvehicule;
                                bool isOk = true;
                                Vehicule vehiculeT=null;
                                do
                                {
                                    do
                                    {
                                        Console.WriteLine("\nVeuillez entrer (CORECTEMENT) le numero du Vehicule choisi: \n");
                                        for (int i = 0; i < listeCh.Count(); i++)
                                        {
                                            Console.WriteLine((i + 1) + ". " + controleur.ListeDesVehicules[i].ToString());
                                        }
                                        Console.Write("\nVotre saisie: ");
                                        numVehiculeINPUT = Console.ReadLine();
                                        Console.Clear();

                                        while (!int.TryParse(numVehiculeINPUT, out INPUTvehicule))
                                        {
                                            Console.WriteLine("\nErreur. Veuillez entrer (CORECTEMENT) le numero du Vehicule choissi: \n");
                                            for (int i = 0; i < controleur.ListeDesVehicules.Count(); i++)
                                            {
                                                Console.WriteLine((i + 1) + ". " + controleur.ListeDesVehicules[i].ToString());
                                            }
                                            Console.Write("\nVotre saisie: ");
                                            numVehiculeINPUT = Console.ReadLine();
                                            Console.Clear();
                                        }

                                    } while (INPUTvehicule < 1 || INPUTvehicule > listeCh.Count());

                                    vehiculeT = controleur.ListeDesVehicules[INPUTchauffeur - 1];
                                    if (vehiculeT is Camion)
                                    {
                                        if (!((Camion)vehiculeT).MatiereTransport.Equals(p.NomProduit) || ((Camion)vehiculeT).VolumeTransportMax < quantité)
                                        {
                                            Console.WriteLine("\nErreur de conformité entre le produit à livrer et le vehicule, ou le vehicule et la quantie à transporter.\nVeuillez choisir un vehicule conforme.\n");
                                        }
                                        else
                                            isOk = false;
                                    }
                                    else
                                        isOk = false;

                                } while (isOk);
                                controleur.addCommande(nom, prenom, villeFrom, p, quantité, chauffeur, vehiculeT, new DateTime(int.Parse(date.Split("/")[0]), int.Parse(date.Split("/")[1]), int.Parse(date.Split("/")[2])));
                                #endregion
                                pressToContinue();
                                continue;
                            case 2:
                                //TODO
                                Console.WriteLine("- Fonctionnalite Modification de commande -\n");
                                pressToContinue();
                                continue;
                            case 3:
                                Console.WriteLine("- Affichage moyenne des prix des commandes -\n");
                                controleur.showAveragePriceCommandes();
                                pressToContinue();
                                continue;
                            case 4:
                                Console.WriteLine("- Fonctionnalite Affichage des commandes -\n");
                                controleur.showCommandes();
                                pressToContinue();
                                continue;
                            default:
                                COMMANDEMENU = false;
                                break;
                        }
                        #endregion

                    }
                    continue;
                #endregion
                case 4:
                    #region GESTIONNAIRE CLIENT 
                    bool PRODUITSMENU = true;
                    while (PRODUITSMENU)
                    {
                        Console.WriteLine("-------------------------- GESTIONNAIRE DE CLIENTS --------------------------\n");
                        Console.WriteLine("Que voulez vous faire ?\n\n1. Afficher produits disponible \n2. Ajouter un nouveau produit \n3. supprimer un produit\nTOUT AUTRE CHIFFRE => Menu principal");
                        Console.Write("\nVotre saisie: ");
                        String ProduitINPUT = Console.ReadLine();

                        Console.Clear();
                        while (!int.TryParse(ProduitINPUT, out INPUT))
                        {
                            Console.WriteLine("Sorry, nous n'avons pas compris votre saisie...\n");
                            Console.WriteLine("Que voulez vous faire ?\n\n1. Afficher produits disponible \n2. Ajouter un nouveau produit \n3. supprimer un produit\nTOUT AUTRE CHIFFRE => Menu principal");
                            Console.Write("\nVotre saisie: ");
                            ProduitINPUT = Console.ReadLine();
                            Console.Clear();
                        }
                        switch (INPUT)
                        {
                            case 1:
                                continue;
                            case 2:
                                continue;
                            case 3:
                                continue;
                            default:
                                PRODUITSMENU = false;
                                break;
                        }
                    }
                        continue;
                    #endregion
                case 5:
                    #region GESTIONNAIRE VEHICULES
                    bool VEHICULESMENU = true;
                    while (VEHICULESMENU)
                    {
                        Console.WriteLine("-------------------------- GESTIONNAIRE DE CLIENTS --------------------------\n");
                        Console.WriteLine("Que voulez vous faire ?\n\n1. Afficher produits disponible \n2. Ajouter un nouveau produit \n3. supprimer un produit\nTOUT AUTRE CHIFFRE => Menu principal");
                        Console.Write("\nVotre saisie: ");
                        String ProduitINPUT = Console.ReadLine();

                        Console.Clear();
                        while (!int.TryParse(ProduitINPUT, out INPUT))
                        {
                            Console.WriteLine("Sorry, nous n'avons pas compris votre saisie...\n");
                            Console.WriteLine("Que voulez vous faire ?\n\n1. Afficher listes des vehicules disponible \n2. Ajouter un nouveau vehicule\n3. supprimer un vehicule\nTOUT AUTRE CHIFFRE => Menu principal");
                            Console.Write("\nVotre saisie: ");
                            ProduitINPUT = Console.ReadLine();
                            Console.Clear();
                        }
                        switch (INPUT)
                        {
                            case 1:
                                continue;
                            case 2:
                                continue;
                            case 3:
                                continue;
                            default:
                                VEHICULESMENU = false;
                                break;
                        }
                    }
                    continue;
                    #endregion
                default:
                    Console.WriteLine("AU REVOIR !");
                    FIRSTMENU = false;
                    break;
                    
            }
        }
        

        #endregion

    }
}
