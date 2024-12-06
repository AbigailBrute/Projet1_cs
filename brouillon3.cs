/*
//Menu utilisateur
    public static bool Menu()
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
        Console.WriteLine("10. Statistiques du fichier");
        Console.WriteLine("11. Quitter");
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
                Stats();
                return true;
            case "11":
                Quitter();
                return false; // Quitte le menu
            default:
                OptionInvalide();
                return true; 
        }
    }

    public static void Main()
    {
        bool Continuer = true;

        while (Continuer)
        {
            Continuer = Menu();
        }
    }
    */