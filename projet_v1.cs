using System;
using System.IO;
//Structure contenant les informations des clients, permettant d'en créer ou en modifier ou les utiliser.
public struct Clients
    {
        public int NumClient;
        public string NomClient;
        public string PrenomClient;
        public string TelClient;
        

        // Constructeur pour initialiser les information d'un client
        public Clients(int Num, string Nom, string Prenom, string Tel)
        {
            NumClient = Num;
            NomClient = Nom;
            PrenomClient = Prenom;
            TelClient = Tel;
        }
            
    }
class ProjetV1
{
    //Procédure pour convertir le nom entré d'un client en masjuscule
    static void Majuscule(ref string Nom)
    {
        Nom = Nom.ToUpper();
    }
    //Procédure pour convertir le prénom entré d'un client avec la première lettre en majuscule et le reste en minuscule
    static void FirstMajuscule(ref string Prenom)
    {
        int i;
        //Convertir le prénom en tableaux de caractères
        char[] TabPrenom = Prenom.ToCharArray();

        // Mettre la première lettre en majuscule
        TabPrenom[0] = char.ToUpper(TabPrenom[0]);

        // Mettre le reste des lettres en minuscules
        for (i = 1; i < TabPrenom.Length; i++)
        {
            TabPrenom[i] = char.ToLower(TabPrenom[i]);
        }

        // Reconstituer la chaîne à partir du tableau
        Prenom = new string(TabPrenom);

    }
    //Procédure option 1 : Ajouter un nouveau client
    static void AjouterClient()
    {
        Console.Write("Entrez le numéro du client : ");
        int Numero = int.Parse(Console.ReadLine());

        Console.Write("Entrez le nom du client : ");
        string Nom = Console.ReadLine();
        Majuscule(ref Nom); // S'assurer que le nom est en majuscule

        Console.Write("Entrez le prénom du client : ");
        string Prenom = Console.ReadLine();
        FirstMajuscule(ref Prenom); // S'assurer que le prénom commence par une majuscule

        Console.Write("Entrez le téléphone du client : ");
        string Tel = Console.ReadLine();

        Clients UnClient = new Clients(Numero, Nom, Prenom, Tel);
        // Enregistrer le client dans le fichier
        using (FileStream Client = new FileStream("Clients.bin", FileMode.Append, FileAccess.Write))
        using (BinaryWriter Ajout = new BinaryWriter(Client))
        {
            Ajout.Write(UnClient.NumClient);
            Ajout.Write(UnClient.NomClient);
            Ajout.Write(UnClient.PrenomClient);
            Ajout.Write(UnClient.TelClient);
        }
        

        Console.WriteLine("Le client " + Nom + " " + Prenom + " a bien été ajouté.");
    }
    //Option 2 : Trouver un client avec son nom
    static void AfficherClient()
    {
        // Demander le nom du client
        Console.Write("Entrez le nom du client à rechercher : ");
        string RechClient = Console.ReadLine().ToUpper(); // Convertir en majuscule pour éviter les erreurs de comparaison

        if (!File.Exists("Clients.bin"))
        {
            Console.WriteLine("Le fichier n'existe pas.");
            return;
        }

        bool ClientTrouve = false; // Indicateur pour savoir si un client a été trouvé
        int NumFiche = 1; // Compteur pour la position dans le fichier
        int Numero;
        string Nom, Prenom, Telephone;

        using (FileStream MonFichier = new FileStream("Clients.bin", FileMode.Open, FileAccess.Read))
        using (BinaryReader Lecture = new BinaryReader(MonFichier))
        {
            Console.WriteLine("Résultats de la recherche :");
            Console.WriteLine("-------------------");

            while (MonFichier.Position < MonFichier.Length)
            {
                try
                {
                    // Lire les données d'un client
                    Clients UnClient = new Clients
                    (
                        Numero = Lecture.ReadInt32(),
                        Nom = Lecture.ReadString(),
                        Prenom = Lecture.ReadString(),
                        Telephone = Lecture.ReadString()
                    );

                    // Vérifier si le nom correspond
                    if (UnClient.NomClient.ToUpper() == RechClient)
                    {
                        // Afficher les informations du client
                        Console.WriteLine("Fiche numéro : " + NumFiche);
                        Console.WriteLine("Numéro : " + UnClient.NumClient);
                        Console.WriteLine("Nom : " + UnClient.NomClient);
                        Console.WriteLine("Prénom : " + UnClient.PrenomClient);
                        Console.WriteLine("Téléphone : " + UnClient.TelClient);
                        Console.WriteLine("-------------------");
                        ClientTrouve = true;
                    }

                    NumFiche++; // Incrémenter le numéro de fiche pour chaque client
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la lecture d'un client : " + ex.Message);
                    break;
                }
            }
        }

        // Si aucun client n'a été trouvé
        if (!ClientTrouve)
        {
            Console.WriteLine("Aucun client trouvé pour le nom spécifié.");
        }

        Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
        Console.ReadKey(); // Pause avant de retourner au menu
    }

    //Option 3 : Afficher tous les clients
    static void AfficherTousClients()
    {
        //Voir comment afficher le numéro de la fiche
        if (!File.Exists("Clients.bin"))
        {
            Console.WriteLine("Le fichier n'existe pas.");
            return;
        }

        int NumFiche = 1; // Compteur pour la position dans le fichier
        int Numero;
        string Nom, Prenom, Telephone;

        using (FileStream MonFichier = new FileStream("Clients.bin", FileMode.Open, FileAccess.Read))
        using (BinaryReader Lecture = new BinaryReader(MonFichier))
        {
            Console.WriteLine("Liste des clients :");
            Console.WriteLine("-------------------");

            while (MonFichier.Position < MonFichier.Length)
            {
                try
                {
                    // Lire les champs d'un client et créer une instance de la structure Clients
                    Clients UnClient = new Clients
                    (
                        Numero = Lecture.ReadInt32(),
                        Nom = Lecture.ReadString(),
                        Prenom = Lecture.ReadString(),
                        Telephone = Lecture.ReadString()
                    );

                    // Vérifiez si la fiche est marquée comme supprimée
                    if (UnClient.NomClient.StartsWith("*"))
                    {
                        continue; // Ignore les clients supprimés logiquement
                    }
                    Console.WriteLine("Fiche numéro : " + NumFiche);
                    Console.WriteLine("Numéro : " + UnClient.NumClient);
                    Console.WriteLine("Nom : " + UnClient.NomClient);
                    Console.WriteLine("Prénom : " + UnClient.PrenomClient);
                    Console.WriteLine("Téléphone : " + UnClient.TelClient);
                    Console.WriteLine("-------------------");

                    NumFiche++; //Incrémentation de la position de la fiche
                }
                catch (EndOfStreamException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la lecture d'un client : " + ex.Message);
                    break;
                }
            }
        
        }

        Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
        Console.ReadKey(); // Pause avant de retourner au menu
    }

    //Option 4 : Affichage du nombre de clients dans le fichier
    static void NombreClients()
    {
        if (!File.Exists("Clients.bin"))
        {
            Console.WriteLine("Le fichier n'existe pas.");
            return;
        }

        int TotalFiches = 0; // Compteur pour toutes les fiches
        int FichesSup = 0; // Compteur pour les fiches supprimées logiquement
        int Fiches = 0; // Compteur pour les fiches non supprimées
        int Numero;
        string Nom, Prenom, Telephone;

        using (FileStream MonFichier = new FileStream("Clients.bin", FileMode.Open, FileAccess.Read))
        using (BinaryReader Lecture = new BinaryReader(MonFichier))
        {
            while (MonFichier.Position < MonFichier.Length)
            {
                try
                {
                    // Lire les données d'un client
                    Clients unClient = new Clients
                    (
                        Numero = Lecture.ReadInt32(),
                        Nom = Lecture.ReadString(),
                        Prenom = Lecture.ReadString(),
                        Telephone = Lecture.ReadString()
                    );

                    // Incrémenter le total des fiches
                    TotalFiches++;

                    // Vérifier si la fiche est supprimée logiquement
                    if (unClient.NomClient.StartsWith("*"))
                    {
                        FichesSup++; //incrémenter les fiches supprimées
                    }
                    else
                    {
                        Fiches++; //incrémenter les fiches non supprimées
                    }
                }
                catch
                {
                    break;
                }
            }
        }

        

        // Afficher le nombre de fiche et autres statistiques
        Console.WriteLine("Statistiques du fichier : ");
        Console.WriteLine("Nombre de fiches existantes    : " + Fiches);
        Console.WriteLine("Nombre total de fiches         : " + TotalFiches);
        Console.WriteLine("Nombre de fiches supprimées    : " + FichesSup);
        

        Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
        Console.ReadKey(); // Pause avant de retourner au menu
    }



    //Menu utilisateur
    static bool Menu()
    {
        //Console.Clear();
        Console.WriteLine("1. Ajouter un nouveau client");
        Console.WriteLine("2. Afficher un client");
        Console.WriteLine("3. Afficher tous les clients");
        Console.WriteLine("4. Afficher le nombre de clients");
        Console.WriteLine("5. Modifier un client");
        Console.WriteLine("6. Supprimer une fiche"); //Suppression logique donc ajout d'un * à la fiche (ligne dans le fichier)
        Console.WriteLine("7. Récupérer une fiche supprimée"); //Retirer le * de ma fiche
        Console.WriteLine("8. Afficher les fiches supprimées"); //Affichage des suppressions logiques
        Console.WriteLine("9. Suppresion définitives des fiches"); //Suppression physique des fiches (effacer les lignes avec un *)
        Console.WriteLine("10. Quitter");
        Console.WriteLine("Choisissez une option :");

        string Choix = Console.ReadLine();
        switch (Choix)
        {
            case "1":
                AjouterClient();
                return true; // Continue le menu
            case "2":
                AfficherClient();
                return true; // Continue le menu
            case "3":
                AfficherTousClients();
                return true; // Continue le menu
            case "4":
                NombreClients();
                return true; // Continue le menu
            case "5":
                Console.WriteLine("Vous avez choisi l'option 2.");
                return true; // Continue le menu
            case "6":
                Console.WriteLine("Vous avez choisi l'option 2.");
                return true; // Continue le menu
            case "7":
                Console.WriteLine("Vous avez choisi l'option 2.");
                return true; // Continue le menu
            case "8":
                Console.WriteLine("Vous avez choisi l'option 2.");
                return true; // Continue le menu
            case "9":
                Console.WriteLine("Vous avez choisi l'option 2.");
                return true; // Continue le menu
            case "10":
                Console.WriteLine("Au revoir !");
                return false; // Quitte le menu
            default:
                Console.WriteLine("Option invalide. Saisissez une option valide : ");
                Console.ReadKey();
                return true; // Continue le menu
        }
    }

    static void Main()
    {
        bool Continuer = true;

        while (Continuer)
        {
            Continuer = Menu();
        }
    }
}