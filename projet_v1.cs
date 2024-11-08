using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
//Structure contenant les informations des clients, permettant d'en créer ou en modifier ou les utiliser.
public struct Clients
    {
        public int NumClients;
        public string NomClient;
        public string PrenomClient;
        public string TelClient;

        // Constructeur pour initialiser les information d'un client
        public Clients(int Num, string Nom, string Prenom, string Tel)
        {
            NumClients = Num;
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
        Console.Write("Entrez le nom du client : ");
        string Nom = Console.ReadLine();
        Majuscule(ref Nom); // S'assurer que le nom est en majuscule

        Console.Write("Entrez le nom du client : ");
        string Prenom = Console.ReadLine();
        FirstMajuscule(ref Prenom); // S'assurer que le prénom commence par une majuscule

        // Enregistrer le nom dans un fichier binaire
        using (FileStream fs = new FileStream("Clients.bin", FileMode.Append))
        {
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                writer.Write(nom);
            }
        }

        Console.WriteLine($"Client ajouté : {nom}");
    }
    //Menu utilisateur
    static bool Menu()
    {
        Console.Clear();
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
                Console.WriteLine("Vous avez choisi l'option 1.");
                return true; // Continue le menu
            case "2":
                Console.WriteLine("Vous avez choisi l'option 2.");
                return true; // Continue le menu
            case "3":
                Console.WriteLine("Vous avez choisi l'option 2.");
                return true; // Continue le menu
            case "4":
                Console.WriteLine("Vous avez choisi l'option 2.");
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
    }
}