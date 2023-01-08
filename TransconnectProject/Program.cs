using TransconnectProject.Model;
using TransconnectProject.Util;
using TransconnectProject.Model.PosteModel;
using TransconnectProject.Controleur;
using TransconnectProject.Controleur.CritereClients;

public class main
{
    public static void Menu() { }
    public static void Main()
    {
        #region InitZone
        var converter = new PosteConverter();
        List<Salarie> lesSalaries = new List<Salarie>();
        List<Client> lesClients = new List<Client>();
        JsonUtil.getJsonSalaries(ref lesSalaries, converter);
        JsonUtil.getJsonClients(ref lesClients);
        TransconnectControleur controleur = new TransconnectControleur(lesSalaries,lesClients);
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
            Console.WriteLine("Choisissez un numero pour parcourir:\n\n1. Gestionnaite de clients\n2. Gestionnaire de salaries\n3. Gestionnaire de commandes\nTOUT AUTRE CHIFFRE => Exit\n");
            Console.Write("Votre entrez: ");
            SFirstINPUT = Console.ReadLine();

            Console.Clear();

            while (!int.TryParse(SFirstINPUT, out INPUT))
            {
                Console.WriteLine("Sorry, nous n'avons pas compris votre saisie...\n");
                Console.WriteLine("Choisissez un numero pour parcourir:\n\n1. Gestionnaite de clients\n2. Gestionnaire de salaries\n3. Gestionnaire de commandes\nTOUT AUTRE CHIFFRE => Exit\n");
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
                                        Console.WriteLine("\nAppuyer entrer pour continuer...");
                                        Console.ReadLine();
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
                                        Console.WriteLine("\nAppuyer entrer pour continuer...");
                                        Console.ReadLine();
                                        break;
                                    case "b":
                                        controleur.showClients(false, new List<TransconnectProject.Controleur.CritereClients.ICritere> { new OrdreVilleCritere() });
                                        Console.WriteLine("\nAppuyer entrer pour continuer...");
                                        Console.ReadLine();
                                        break;
                                    case "c":
                                        controleur.showClients(false, new List<TransconnectProject.Controleur.CritereClients.ICritere> { new OrdreMontantCumule() });
                                        Console.WriteLine("\nAppuyer entrer pour continuer...");
                                        Console.ReadLine();
                                        break;
                                    case "ab":
                                    case "ba":
                                        controleur.showClients(false, new List<TransconnectProject.Controleur.CritereClients.ICritere> { new OrdreAlphaCritere(), new OrdreVilleCritere() });
                                        Console.WriteLine("\nAppuyer entrer pour continuer...");
                                        Console.ReadLine();
                                        break;
                                    case "ac":
                                    case "ca":
                                        controleur.showClients(false, new List<TransconnectProject.Controleur.CritereClients.ICritere> { new OrdreAlphaCritere(), new OrdreMontantCumule() });
                                        Console.WriteLine("\nAppuyer entrer pour continuer...");
                                        Console.ReadLine();
                                        break;
                                    case "bc":
                                    case "cb":
                                        controleur.showClients(false, new List<TransconnectProject.Controleur.CritereClients.ICritere> { new OrdreMontantCumule(), new OrdreVilleCritere() });
                                        Console.WriteLine("\nAppuyer entrer pour continuer...");
                                        Console.ReadLine();
                                        break;
                                    case "abc":
                                    case "acb":
                                    case "bac":
                                    case "bca":
                                    case "cab":
                                    case "cba":
                                        controleur.showClients(false, new List<TransconnectProject.Controleur.CritereClients.ICritere> { new OrdreAlphaCritere(), new OrdreVilleCritere(), new OrdreMontantCumule() });
                                        Console.WriteLine("\nAppuyer entrer pour continuer...");
                                        Console.ReadLine();
                                        break;
                                    default:
                                        controleur.showClients();
                                        Console.WriteLine("\nAppuyer entrer pour continuer...");
                                        Console.ReadLine();
                                        break;
                                }
                                Console.Clear();
                                break;
                            #endregion
                            #region Ajout client
                            case 2:
                                //TODO: MENU ADD CLIENT
                                Console.WriteLine("Ajout d'un client\n");
                                Client newClient = Client.createClient();
                                controleur.addClient(newClient);
                                Console.WriteLine("\nAppuyer entrer pour continuer...");
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            #endregion
                            #region Update client
                            case 3:
                                //TODO: MENU ADD CLIENT
                                Console.WriteLine("Modification d'un client\n");
                                break;
                            #endregion
                            #region Supprimation client
                            case 4:
                                Console.WriteLine("Supprimation d'un client");
                                Console.Write("\nveuillez saisir le nom du client: ");
                                string nom = Console.ReadLine();
                                Console.Write("\nveuillez saisir le prenom du client: ");
                                string prenom = Console.ReadLine();
                                controleur.deleteClient(nom, prenom);
                                Console.WriteLine("\nAppuyer entrer pour continuer...");
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            #endregion
                            //MODULE STATISTIQUE
                            #region Average commande client
                            case 5:
                                Console.WriteLine("- Fonctionnalioté Moyenne achat par client -\n");
                                controleur.showAverageAchatCompteClient();
                                Console.WriteLine("\nAppuyer entrer pour continuer...");
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            #endregion
                            //MODULE STATISTIQUE
                            #region Liste commandes par client
                            case 6:
                                Console.WriteLine("- Fonctionnalioté liste commandes client -\n");
                                controleur.showClientsListCommandes();
                                Console.WriteLine("\nAppuyer entrer pour continuer...");
                                Console.ReadLine();
                                Console.Clear();
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
                    bool SALARIESMENU = true;
                    while (SALARIESMENU)
                    {
                        Console.WriteLine("-------------------------- GESTIONNAIRE DE SALARIES --------------------------");
                        Console.WriteLine("Que voulez vous faire ?\n\n1. Afficher l'organigramme \n2. Embaucher un salarie \n3. Licencier un salarie\n4. Afficher nombre de livraison par chauffeur\nTOUT AUTRE CHIFFRE => Menu principal \n");
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
                        String CommandeINPUT = Console.ReadLine();
                        Console.Clear();
                        while (!int.TryParse(CommandeINPUT, out INPUT))
                        {
                            Console.WriteLine("Sorry, nous n'avons pas compris votre saisie...\n");
                            Console.WriteLine("Que voulez vous faire ?\n\n1. Effectuer une commande \n2. Modifier une commande \n3. Afficher moyenne des prix des commandes\n4. Afficher les commandes\nTOUT AUTRE CHIFFRE => Menu principal \n");
                            CommandeINPUT = Console.ReadLine();
                            Console.Clear();
                        }
                        //Switch part
                        #region COMMANDE SWITCH
                        switch (INPUT)
                        {
                            default:
                                COMMANDEMENU = false;
                                break;
                        }
                        #endregion

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
