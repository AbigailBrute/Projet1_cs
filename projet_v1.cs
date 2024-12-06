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
        Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
        Console.ReadKey(); // Pause avant de retourner au menu
    }
    //Option 2 : Trouver un client avec son nom
    static void AfficherClient()
    {
        if (!File.Exists("Clients.bin"))
        {
            Console.WriteLine("Le fichier n'existe pas. Commencez par ajouter un client.");
            Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
            Console.ReadKey();
            return;
        }
        // Demander le nom du client
        Console.Write("Entrez le nom du client à rechercher : ");
        string RechClient = Console.ReadLine().ToUpper(); // Convertir en majuscule pour éviter les erreurs de comparaison

        bool ClientTrouve = false; // Indicateur pour savoir si un client a été trouvé
        //Variable pour obtenir le numéro de la fiche client
        int NumFiche = 1;
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
                        // Afficher les informations du client
                        Console.WriteLine("Fiche numéro : " + NumFiche);
                        Console.WriteLine("Numéro : " + UnClient.NumClient);
                        Console.WriteLine("Nom : " + UnClient.NomClient);
                        Console.WriteLine("Prénom : " + UnClient.PrenomClient);
                        Console.WriteLine("Téléphone : " + UnClient.TelClient);
                        Console.WriteLine("-------------------");
                        ClientTrouve = true;
                    }

                    NumFiche++; // Incrémenter le numéro de fiche après chaque lecture
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
            Console.WriteLine("Le fichier n'existe pas. Commencez par ajouter un client.");
            Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
            Console.ReadKey();
            return;
        }
        //Variable pour obtenir la numéro de la fiche client
        int NumFiche = 1;
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

                    NumFiche++; // Incrémenter le numéro de fiche après chaque lecture
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
            Console.WriteLine("Le fichier n'existe pas. Commencez par ajouter un client.");
            Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
            Console.ReadKey();
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
            Console.WriteLine("Le fichier n'existe pas. Commencez par ajouter un client.");
            Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
            Console.ReadKey();
            return;
        }

        Console.Write("Entrez le numéro de la fiche à modifier : ");
        if (!int.TryParse(Console.ReadLine(), out int NumFicheClient))
        {
            Console.WriteLine("La valeur saisie n'est pas un numéro valide.");
            Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
            Console.ReadKey();
            return;
        }

        List<(int Numero, string Nom, string Prenom, string Telephone)> ListClients = new List<(int, string, string, string)>();

        // Lire toutes les fiches
        using (FileStream MonFichier = new FileStream("Clients.bin", FileMode.Open, FileAccess.Read))
        using (BinaryReader Lecture = new BinaryReader(MonFichier))
        {
            while (MonFichier.Position < MonFichier.Length)
            {
                try
                {
                    int NumeroClient = Lecture.ReadInt32();
                    string Nom = Lecture.ReadString();
                    string Prenom = Lecture.ReadString();
                    string Telephone = Lecture.ReadString();

                    ListClients.Add((NumeroClient, Nom, Prenom, Telephone));
                }
                catch (EndOfStreamException)
                {
                    break;
                }
            }
        }

        if (NumFicheClient < 1 || NumFicheClient > ListClients.Count)
        {
            Console.WriteLine("Fiche non trouvée.");
            Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
            Console.ReadKey();
            return;
        }

        // Modifier la fiche cible
        var Client = ListClients[NumFicheClient - 1];
        Console.WriteLine("Fiche à modifier : ");
        Console.WriteLine("Fiche actuelle : " + NumFicheClient);
        Console.WriteLine("Numéro client : " + Client.Numero);
        Console.WriteLine("Nom : " + Client.Nom);
        Console.WriteLine("Prénom : " + Client.Prenom);
        Console.WriteLine("Téléphone : " + Client.Telephone);
        Console.WriteLine("-------------------");

        // Demande du nouveau nom
        Console.Write("Nouveau nom " + "(" + Client.Nom + ") : ");
        string NouveauNom = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(NouveauNom)) 
        {
            NouveauNom = Client.Nom;
        }
        else 
        {
            Majuscule(ref NouveauNom); // Appliquer la procédure Majuscule
        }

        // Demande du nouveau prénom
        Console.Write("Nouveau prénom " + "(" + Client.Prenom + ") : ");
        string NouveauPrenom = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(NouveauPrenom)) 
        {
            NouveauPrenom = Client.Prenom;
        }
        else 
        {
            FirstMajuscule(ref NouveauPrenom); // Appliquer la procédure FirstMajuscule
        }

        // Demande du nouveau numéro de téléphone
        Console.Write("Nouveau téléphone " + "(" + Client.Telephone + ") : ");
        string NouveauTelephone = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(NouveauTelephone)) 
        {
            NouveauTelephone = Client.Telephone;
        }

        Console.WriteLine("\nRésumé des modifications proposées :");
        Console.WriteLine("Nom : " + Client.Nom + " → " + NouveauNom);
        Console.WriteLine("Prénom : " + Client.Prenom + " → " + NouveauPrenom);
        Console.WriteLine("Téléphone : " + Client.Telephone + " → " + NouveauTelephone);
        Console.Write("Confirmez-vous les modifications ? (o/n) : ");
        string Confirmation = Console.ReadLine()?.ToLower();

        if (Confirmation == "o" || Confirmation == "oui")
        {
            ListClients[NumFicheClient - 1] = (Client.Numero, NouveauNom, NouveauPrenom, NouveauTelephone);

            // Réécrire tout le fichier
            using (FileStream MonFichier = new FileStream("Clients.bin", FileMode.Create, FileAccess.Write))
            using (BinaryWriter Ecriture = new BinaryWriter(MonFichier))
            {
                foreach (var c in ListClients)
                {
                    Ecriture.Write(c.Numero);
                    Ecriture.Write(c.Nom);
                    Ecriture.Write(c.Prenom);
                    Ecriture.Write(c.Telephone);
                }
            }

            Console.WriteLine("Données du client modifiées avec succès.");
        }
        else
        {
            Console.WriteLine("Modifications annulées.");
        }

        Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
        Console.ReadKey();
    }

//Option 6 : Supprimer logiquement un client
static void SupprimerClient()
{
    if (!File.Exists("Clients.bin"))
    {
        Console.WriteLine("Le fichier n'existe pas.");
        Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
        Console.ReadKey(); // Pause avant de retourner au menu
        return;
    }

    Console.Write("Entrez le numéro de la fiche à supprimer : ");
    if (!int.TryParse(Console.ReadLine(), out int numFicheUtilisateur))
    {
        Console.WriteLine("La valeur saisie n'est pas un numéro valide.");
        Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
        Console.ReadKey(); // Pause avant de retourner au menu
        return;
    }

    bool ficheTrouvee = false;
    long positionDebut; // Position de début de la fiche trouvée

    List<Clients> clients = new List<Clients>(); // Liste pour stocker les clients

    using (FileStream monFichier = new FileStream("Clients.bin", FileMode.Open, FileAccess.ReadWrite))
    using (BinaryReader lecture = new BinaryReader(monFichier))
    {
        int compteurFiche = 1;

        while (monFichier.Position < monFichier.Length)
        {
            try
            {
                positionDebut = monFichier.Position;

                // Lire les données de la fiche
                int numero = lecture.ReadInt32();
                string nom = lecture.ReadString();
                string prenom = lecture.ReadString();
                string telephone = lecture.ReadString();

                if (compteurFiche == numFicheUtilisateur)
                {
                    ficheTrouvee = true;
                    Console.WriteLine("\nDonnées actuelles de la fiche :");
                    Console.WriteLine($"Fiche numéro : {compteurFiche}");
                    Console.WriteLine($"Numéro : {numero}");
                    Console.WriteLine($"Nom : {nom}");
                    Console.WriteLine($"Prénom : {prenom}");
                    Console.WriteLine($"Téléphone : {telephone}");
                    Console.WriteLine("-------------------");

                    Console.Write("Confirmez-vous la suppression de ce client ? (o/n) : ");
                    string confirmation = Console.ReadLine()?.ToLower();

                    if (confirmation == "o" || confirmation == "oui")
                    {
                        // Ajouter un * devant le nom pour marquer comme supprimé
                        string nouveauNom = "*" + nom;

                        // Ajouter à la liste des clients (sans modifier le fichier pour l'instant)
                        clients.Add(new Clients(numero, nouveauNom, prenom, telephone));
                        Console.WriteLine("Le client a été supprimé logiquement avec succès.");
                    }
                    else
                    {
                        Console.WriteLine("Suppression annulée.");
                        clients.Add(new Clients(numero, nom, prenom, telephone)); // Ajoute sans modifier
                    }
                }
                else
                {
                    clients.Add(new Clients(numero, nom, prenom, telephone)); // Ajoute les autres clients
                }

                compteurFiche++;
            }
            catch (EndOfStreamException)
            {
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la lecture d'une fiche : " + ex.Message);
                break;
            }
        }

        if (!ficheTrouvee)
        {
            Console.WriteLine("Fiche non trouvée.");
        }
    }

    // Réécriture complète du fichier avec la liste mise à jour
    using (FileStream monFichier = new FileStream("Clients.bin", FileMode.Create, FileAccess.Write))
    using (BinaryWriter ecriture = new BinaryWriter(monFichier))
    {
        foreach (var client in clients)
        {
            ecriture.Write(client.NumClient);
            ecriture.Write(client.NomClient);
            ecriture.Write(client.PrenomClient);
            ecriture.Write(client.TelClient);
        }
    }

    Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
    Console.ReadKey(); // Pause avant de retourner au menu
}


static void RecupererFicheSupprimee()
{
    if (!File.Exists("Clients.bin"))
    {
        Console.WriteLine("Le fichier n'existe pas.");
        Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
        Console.ReadKey();
        return;
    }

    Console.Write("Entrez le nom du client à récupérer : ");
    string nomRecherche = Console.ReadLine();
    Majuscule(ref nomRecherche); // Convertir en majuscules pour éviter la casse

    bool ficheTrouvee = false;

    List<Clients> clients = new List<Clients>(); // Liste temporaire pour stocker toutes les fiches

    using (FileStream monFichier = new FileStream("Clients.bin", FileMode.Open, FileAccess.Read))
    using (BinaryReader lecture = new BinaryReader(monFichier))
    {
        while (monFichier.Position < monFichier.Length)
        {
            try
            {
                // Lire les données du client
                int numero = lecture.ReadInt32();
                string nom = lecture.ReadString();
                string prenom = lecture.ReadString();
                string telephone = lecture.ReadString();

                // Si le nom commence par "*" et correspond au nom recherché
                if (nom.StartsWith("*") && nom.Substring(1) == nomRecherche)
                {
                    ficheTrouvee = true;

                    Console.WriteLine("Fiche trouvée :");
                    Console.WriteLine($"Numéro : {numero}");
                    Console.WriteLine($"Nom : {nom}");
                    Console.WriteLine($"Prénom : {prenom}");
                    Console.WriteLine($"Téléphone : {telephone}");
                    Console.WriteLine("-------------------");

                    Console.Write("Voulez-vous récupérer cette fiche ? (O/N) : ");
                    string confirmation = Console.ReadLine().ToUpper();

                    if (confirmation == "O")
                    {
                        // Remettre le nom original (sans '*')
                        nom = nomRecherche;
                        Console.WriteLine("La fiche a été récupérée avec succès.");
                    }
                    else
                    {
                        Console.WriteLine("Récupération annulée.");
                    }
                }

                // Ajouter la fiche (modifiée ou non) à la liste
                clients.Add(new Clients(numero, nom, prenom, telephone));
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

    if (!ficheTrouvee)
    {
        Console.WriteLine("Aucune fiche correspondant au nom spécifié n'a été trouvée.");
    }

    // Réécriture du fichier avec toutes les fiches mises à jour
    using (FileStream monFichier = new FileStream("Clients.bin", FileMode.Create, FileAccess.Write))
    using (BinaryWriter ecriture = new BinaryWriter(monFichier))
    {
        foreach (var client in clients)
        {
            ecriture.Write(client.NumClient);
            ecriture.Write(client.NomClient);
            ecriture.Write(client.PrenomClient);
            ecriture.Write(client.TelClient);
        }
    }

    Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
    Console.ReadKey();
}

//Option 3 : Afficher tous les clients
    static void AfficherSupprimer()
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
            Console.WriteLine("Liste des clients supprimés :");
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
                       Console.WriteLine("Fiche numéro : " + NumFiche);
                        Console.WriteLine("Numéro : " + UnClient.NumClient);
                        Console.WriteLine("Nom : " + UnClient.NomClient);
                        Console.WriteLine("Prénom : " + UnClient.PrenomClient);
                        Console.WriteLine("Téléphone : " + UnClient.TelClient);
                        Console.WriteLine("-------------------");
                    }
                    else
                    {
                         continue; // Ignore les clients non supprimés
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

        Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
        Console.ReadKey(); // Pause avant de retourner au menu
    }
    // 5. Suppression des fiches avec un '*' devant le nom
    public static void SupprimerClientDefinitive()
    {
        if (!File.Exists("Clients.bin"))
        {
            Console.WriteLine("Le fichier n'existe pas.");
            return;
        }

        using (FileStream MonFichier = new FileStream("Clients.bin", FileMode.Open, FileAccess.Read))
        using (BinaryReader ReaderBin = new BinaryReader(MonFichier))
        {
            // Mettre en place le fichier temporaire et l’ouvrir en écriture
            using (FileStream MonFichierTemp = new FileStream("Clients.bin" + ".temp", FileMode.Create, FileAccess.Write))
            using (BinaryWriter WriterBinTemp = new BinaryWriter(MonFichierTemp))
            {
                while (MonFichier.Position < MonFichier.Length)
                {
                    try
                    {
                        // Lire les données d'une fiche
                        int Numero = ReaderBin.ReadInt32();
                        string Nom = ReaderBin.ReadString();
                        string Prenom = ReaderBin.ReadString();
                        string Telephone = ReaderBin.ReadString();

                        // Si le nom ne commence pas par '*', conserver la fiche
                        if (!Nom.StartsWith("*"))
                        {
                            WriterBinTemp.Write(Numero);
                            WriterBinTemp.Write(Nom);
                            WriterBinTemp.Write(Prenom);
                            WriterBinTemp.Write(Telephone);
                        }
                    }
                    catch (EndOfStreamException)
                    {
                        break; // Fin du fichier atteinte
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erreur lors de la lecture : " + ex.Message);
                        break;
                    }
                }
            }
        }

        // Supprimer le fichier original
        File.Delete("Clients.bin");

        // Renommer le fichier temporaire avec le nom du fichier original
        File.Move("Clients.bin" + ".temp", "Clients.bin");
        Console.WriteLine("Fiches supprimées");

        Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
        Console.ReadKey(); // Pause avant de retourner au menu

    }

    static void Quitter()
    {
        Console.WriteLine("Merci d'avoir utilisé l'application. À bientôt !");
        System.Threading.Thread.Sleep(1500); // Pause pour afficher le message (1,5 seconde)
        Console.Clear(); // Nettoie le terminal
    }

    static void OptionInvalide()
    {
        Console.WriteLine("Option invalide. Saisissez une option valide : ");
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
                return true; 
            case "3":
                AfficherTousClients();
                return true; 
            case "4":
                NombreClients();
                return true; 
            case "5":
                ModifierClient();
                return true; 
            case "6":
                SupprimerClient();
                return true; 
            case "7":
                RecupererFicheSupprimee();
                return true; 
            case "8":
                AfficherSupprimer();
                return true; 
            case "9":
                SupprimerClientDefinitive();
                return true; 
            case "10":
                Quitter();
                return false; // Quitte le menu
            default:
                OptionInvalide();
                return true; 
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