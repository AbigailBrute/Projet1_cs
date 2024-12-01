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
        //Variables pour obtenir la numéro de la fiche client
        int NumFiche, NbElements;
        long Position;
        //Variables pour lire les données du fichier
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
                        //Calcul position fiche
                        Position = MonFichier.Position;
                        NbElements = sizeof(int) + Nom.Length + Prenom.Length + Telephone.Length;
                        NumFiche = (int)(Position / NbElements);
                        // Afficher les informations du client
                        Console.WriteLine("Fiche numéro : " + NumFiche);
                        Console.WriteLine("Numéro : " + UnClient.NumClient);
                        Console.WriteLine("Nom : " + UnClient.NomClient);
                        Console.WriteLine("Prénom : " + UnClient.PrenomClient);
                        Console.WriteLine("Téléphone : " + UnClient.TelClient);
                        Console.WriteLine("-------------------");
                        ClientTrouve = true;
                    }
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
        if (!File.Exists("Clients.bin"))
        {
            Console.WriteLine("Le fichier n'existe pas.");
            return;
        }
        //Variables pour obtenir la numéro de la fiche client
        int NumFiche, NbElements;
        long Position;
        //Variables pour lire les données clients
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

                    // Obtenir la position actuelle dans le fichier pour calculer le numéro de la fiche
                    Position = MonFichier.Position;
                    NbElements = sizeof(int) + Nom.Length + Prenom.Length + Telephone.Length;
                    NumFiche = (int)(Position / NbElements);

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

        int Fiches = 0; // Compteur pour les fiches

        //Variables pour lire les données clients
        int Numero;
        string Nom, Prenom, Telephone;

        using (FileStream MonFichier = new FileStream("Clients.bin", FileMode.Open, FileAccess.Read))
        using (BinaryReader Lecture = new BinaryReader(MonFichier))
        {

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
                        continue;
                    }
                    else
                    {
                        Fiches++;
                    }
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

        // Afficher le nombre de fiche et autres statistiques
        Console.WriteLine("Nombre de fiches existantes dans le fichier Clients : " + Fiches);
        

        Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
        Console.ReadKey(); // Pause avant de retourner au menu
    }
    
    //Option 5 : Modifier un client existant
static void ModifierClient()
{
    if (!File.Exists("Clients.bin"))
    {
        Console.WriteLine("Le fichier n'existe pas.");
        Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
        Console.ReadKey();
        return;
    }

    Console.Write("Entrez le numéro de la fiche à modifier : ");
    if (!int.TryParse(Console.ReadLine(), out int numFicheUtilisateur))
    {
        Console.WriteLine("La valeur saisie n'est pas un numéro valide.");
        Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
        Console.ReadKey();
        return;
    }

    bool ficheTrouvee = false; // Indicateur de fiche trouvée
    long positionDebut = 0;   // Position de début pour écriture
    int compteurFiche = 1;    // Compteur commence à 1

    using (FileStream monFichier = new FileStream("Clients.bin", FileMode.Open, FileAccess.ReadWrite))
    using (BinaryReader lecture = new BinaryReader(monFichier))
    using (BinaryWriter ecriture = new BinaryWriter(monFichier))
    {
        while (monFichier.Position < monFichier.Length)
        {
            try
            {
                positionDebut = monFichier.Position; // Sauvegarde de la position actuelle

                // Lecture des données
                int numero = lecture.ReadInt32();
                string nom = lecture.ReadString();
                string prenom = lecture.ReadString();
                string telephone = lecture.ReadString();

                // Ignore les fiches logiquement supprimées
                if (nom.StartsWith("*")) continue;

                if (compteurFiche == numFicheUtilisateur)
                {
                    ficheTrouvee = true;

                    // Affichage des données actuelles
                    Console.WriteLine($"Fiche actuelle : {compteurFiche}");
                    Console.WriteLine($"Numéro : {numero}");
                    Console.WriteLine($"Nom : {nom}");
                    Console.WriteLine($"Prénom : {prenom}");
                    Console.WriteLine($"Téléphone : {telephone}");
                    Console.WriteLine("-------------------");

                    // Demande des nouvelles données
                    Console.Write($"Nouveau nom ({nom}) : ");
                    string nouveauNom = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(nouveauNom))
                    {
                        nouveauNom = nom;
                    }
                    else
                    {
                        Majuscule(ref nouveauNom);
                    }

                    Console.Write($"Nouveau prénom ({prenom}) : ");
                    string nouveauPrenom = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(nouveauPrenom))
                    {
                        nouveauPrenom = prenom;
                    }
                    else
                    {
                        FirstMajuscule(ref nouveauPrenom);
                    }
                    Console.Write($"Nouveau téléphone ({telephone}) : ");
                    string nouveauTelephone = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(nouveauTelephone)) nouveauTelephone = telephone;

                    // Confirmation
                    Console.WriteLine("\nRésumé des modifications proposées :");
                    Console.WriteLine($"Nom : {nom} → {nouveauNom}");
                    Console.WriteLine($"Prénom : {prenom} → {nouveauPrenom}");
                    Console.WriteLine($"Téléphone : {telephone} → {nouveauTelephone}");
                    Console.Write("Confirmez-vous les modifications ? (o/n) : ");
                    string confirmation = Console.ReadLine()?.ToLower();

                    if (confirmation == "o" || confirmation == "oui")
                    {
                        // Réécriture de la fiche
                        monFichier.Seek(positionDebut, SeekOrigin.Begin);
                        ecriture.Write(numero);        // Réécrire le numéro
                        ecriture.Write(nouveauNom);   // Écrire le nouveau nom
                        ecriture.Write(nouveauPrenom);// Écrire le nouveau prénom
                        ecriture.Write(nouveauTelephone); // Écrire le nouveau téléphone
                        Console.WriteLine("Données du client modifiées avec succès.");
                    }
                    else
                    {
                        Console.WriteLine("Modifications annulées.");
                    }

                    break; // On sort de la boucle, la fiche est traitée
                }

                compteurFiche++; // Passage à la fiche suivante
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

        if (!ficheTrouvee)
        {
            Console.WriteLine("Fiche non trouvée.");
        }
    }

    Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
    Console.ReadKey();
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
                ModifierClient();
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