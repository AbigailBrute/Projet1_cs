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
        
        long Position;//Variable pour calculer la postion du fichier
        int NbElements, NumFiche;

        int TotalFiches = 0; // Compteur pour toutes les fiches
        int FichesSup = 0; // Compteur pour les fiches supprimées logiquement
        int Fiches = 0; // Compteur pour les fiches non supprimées

        //Variables pour lire les données clients
        int Numero;
        string Nom, Prenom, Telephone;

        using (FileStream MonFichier = new FileStream("Clients.bin", FileMode.Open, FileAccess.Read))
        using (BinaryReader Lecture = new BinaryReader(MonFichier))
        {
            MonFichier.Seek(0, SeekOrigin.End);
            Position = MonFichier.Position;
            TotalFiches = (int)(Position / sizeof(int));
            MonFichier.Seek(0, SeekOrigin.Begin);

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
                        FichesSup++;
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
        Console.WriteLine("Statistiques du fichier : ");
        Console.WriteLine("Nombre de fiches existantes    : " + Fiches);
        Console.WriteLine("Nombre total de fiches         : " + TotalFiches);
        Console.WriteLine("Nombre de fiches supprimées    : " + FichesSup);
        

        Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
        Console.ReadKey(); // Pause avant de retourner au menu
    }

    //Option 5 : Modifier un client existant
    static void ModifierClient()
    {
    if (!File.Exists("Clients.bin"))
    {
        Console.WriteLine("Le fichier n'existe pas.");
        return;
    }

    // Demander à l'utilisateur le numéro de la fiche à modifier (changer pour TryParse)
    Console.Write("Entrez le numéro de la fiche à modifier : ");
    int numeroFiche = int.Parse(Console.ReadLine());

    

    // Utiliser FileStream pour lire et modifier le fichier binaire
    using (FileStream MonFichier = new FileStream("Clients.bin", FileMode.Open, FileAccess.ReadWrite))
    using (BinaryReader Lecture = new BinaryReader(MonFichier))
    using (BinaryWriter Ecriture = new BinaryWriter(MonFichier))
    {
        bool ficheTrouvee = false; // Indicateur pour savoir si la fiche est trouvée
        int NumFiche = 1; // Compteur pour la position des fiches

        while (MonFichier.Position < MonFichier.Length)
        {
            // Lire chaque champ dans une variable
            int numClient = Lecture.ReadInt32();        // Numéro du client
            string nomClient = Lecture.ReadString();    // Nom
            string prenomClient = Lecture.ReadString(); // Prénom
            string telClient = Lecture.ReadString();    // Téléphone

            // Vérifier si la fiche est marquée comme supprimée logiquement (nom commence par *)
            if (nomClient.StartsWith("*"))
            {
                NumFiche++; // Ignorer les fiches supprimées
                continue;
            }

            // Vérifier si c'est la fiche à modifier (par numéro de fiche)
            if (NumFiche == numeroFiche)
            {
                ficheTrouvee = true;

                // Afficher les données actuelles
                Console.WriteLine($"Fiche actuelle : {NumFiche}");
                Console.WriteLine($"Numéro : {numClient}");
                Console.WriteLine($"Nom : {nomClient}");
                Console.WriteLine($"Prénom : {prenomClient}");
                Console.WriteLine($"Téléphone : {telClient}");
                Console.WriteLine("-------------------");

                // Demander les nouvelles informations (laisser vide pour conserver la valeur actuelle)
                Console.Write("Entrez le nouveau nom (laisser vide pour conserver l'ancien) : ");
                string nouveauNom = Console.ReadLine();
                if (string.IsNullOrEmpty(nouveauNom)) nouveauNom = nomClient;

                Console.Write("Entrez le nouveau prénom (laisser vide pour conserver l'ancien) : ");
                string nouveauPrenom = Console.ReadLine();
                if (string.IsNullOrEmpty(nouveauPrenom)) nouveauPrenom = prenomClient;

                Console.Write("Entrez le nouveau téléphone (laisser vide pour conserver l'ancien) : ");
                string nouveauTel = Console.ReadLine();
                if (string.IsNullOrEmpty(nouveauTel)) nouveauTel = telClient;

                // Confirmer les modifications
                Console.WriteLine("\nRésumé des modifications :");
                Console.WriteLine($"Nom : {nouveauNom} (ancien : {nomClient})");
                Console.WriteLine($"Prénom : {nouveauPrenom} (ancien : {prenomClient})");
                Console.WriteLine($"Téléphone : {nouveauTel} (ancien : {telClient})");
                Console.Write("Confirmer la modification ? (O/N) : ");
                string confirmation = Console.ReadLine().ToUpper();

                if (confirmation == "O")
                {
                    // Réécrire les données dans le fichier à la position correcte
                    long position = MonFichier.Position - (Lecture.BaseStream.Position - MonFichier.Position);

                    // Repositionner le curseur du fichier à la position de la fiche
                    MonFichier.Seek(position, SeekOrigin.Begin);

                    // Réécrire les données du client modifié
                    Ecriture.Write(numClient);
                    Ecriture.Write(nouveauNom);
                    Ecriture.Write(nouveauPrenom);
                    Ecriture.Write(nouveauTel);

                    Console.WriteLine("\nClient modifié avec succès !");
                }
                else
                {
                    Console.WriteLine("\nModification annulée.");
                }

                break; // Sortir de la boucle dès que la fiche est trouvée
            }

            NumFiche++; // Incrémenter le numéro de fiche pour la prochaine boucle
        }

        if (!ficheTrouvee)
        {
            Console.WriteLine("Aucune fiche trouvée avec ce numéro.");
        }
    }

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