//option 5
static void ModifierClient()
{
    if (!File.Exists("Clients.bin"))
    {
        Console.WriteLine("Le fichier n'existe pas.");
        return;
    }

    Console.Write("Entrez le numéro de la fiche à modifier : ");
    if (!int.TryParse(Console.ReadLine(), out int numeroFiche))
    {
        Console.WriteLine("Numéro de fiche invalide.");
        return;
    }

    bool ficheTrouvee = false; // Indique si la fiche demandée existe
    List<Clients> clients = new List<Clients>(); // Stocker toutes les fiches temporairement

    using (FileStream MonFichier = new FileStream("Clients.bin", FileMode.Open, FileAccess.Read))
    using (BinaryReader Lecture = new BinaryReader(MonFichier))
    {
        int compteurFiche = 1;

        while (MonFichier.Position < MonFichier.Length)
        {
            try
            {
                // Lire les données d'un client
                Clients unClient = new Clients
                (
                    Lecture.ReadInt32(),       // Numéro du client
                    Lecture.ReadString(),      // Nom
                    Lecture.ReadString(),      // Prénom
                    Lecture.ReadString()       // Téléphone
                );

                // Ajouter la fiche à la liste pour réécriture ultérieure
                clients.Add(unClient);

                if (compteurFiche == numeroFiche)
                {
                    ficheTrouvee = true;

                    // Afficher les informations actuelles
                    Console.WriteLine("Fiche trouvée. Données actuelles :");
                    Console.WriteLine($"Numéro : {unClient.NumClients}");
                    Console.WriteLine($"Nom : {unClient.NomClient}");
                    Console.WriteLine($"Prénom : {unClient.PrenomClient}");
                    Console.WriteLine($"Téléphone : {unClient.TelClient}");
                    Console.WriteLine("-------------------");

                    // Saisir les nouvelles valeurs
                    Console.Write("Entrez le nouveau numéro (ou appuyez sur Entrée pour conserver) : ");
                    string nouveauNumero = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(nouveauNumero))
                    {
                        unClient.NumClients = int.Parse(nouveauNumero);
                    }

                    Console.Write("Entrez le nouveau nom (ou appuyez sur Entrée pour conserver) : ");
                    string nouveauNom = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(nouveauNom))
                    {
                        unClient.NomClient = nouveauNom.ToUpper();
                    }

                    Console.Write("Entrez le nouveau prénom (ou appuyez sur Entrée pour conserver) : ");
                    string nouveauPrenom = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(nouveauPrenom))
                    {
                        unClient.PrenomClient = nouveauPrenom;
                    }

                    Console.Write("Entrez le nouveau téléphone (ou appuyez sur Entrée pour conserver) : ");
                    string nouveauTel = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(nouveauTel))
                    {
                        unClient.TelClient = nouveauTel;
                    }

                    // Demander confirmation
                    Console.WriteLine("Confirmez-vous ces modifications ? (O/N)");
                    string confirmation = Console.ReadLine().ToUpper();
                    if (confirmation == "O")
                    {
                        clients[compteurFiche - 1] = unClient; // Mettre à jour la fiche dans la liste
                        Console.WriteLine("Fiche modifiée avec succès !");
                    }
                    else
                    {
                        Console.WriteLine("Modification annulée.");
                    }
                }

                compteurFiche++;
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
        Console.WriteLine("Fiche introuvable.");
        return;
    }

    // Réécriture complète du fichier binaire avec les données mises à jour
    using (FileStream MonFichier = new FileStream("Clients.bin", FileMode.Create, FileAccess.Write))
    using (BinaryWriter Ecriture = new BinaryWriter(MonFichier))
    {
        foreach (Clients client in clients)
        {
            Ecriture.Write(client.NumClients);
            Ecriture.Write(client.NomClient);
            Ecriture.Write(client.PrenomClient);
            Ecriture.Write(client.TelClient);
        }
    }
}
//option 6
static void SupprimerFiche()
{
    if (!File.Exists("Clients.bin"))
    {
        Console.WriteLine("Le fichier n'existe pas.");
        return;
    }

    Console.Write("Entrez le numéro de la fiche à supprimer : ");
    if (!int.TryParse(Console.ReadLine(), out int numeroFiche))
    {
        Console.WriteLine("Numéro de fiche invalide.");
        return;
    }

    bool ficheTrouvee = false; // Indique si la fiche demandée existe
    List<Clients> clients = new List<Clients>(); // Stocker temporairement toutes les fiches

    using (FileStream MonFichier = new FileStream("Clients.bin", FileMode.Open, FileAccess.Read))
    using (BinaryReader Lecture = new BinaryReader(MonFichier))
    {
        int compteurFiche = 1;

        while (MonFichier.Position < MonFichier.Length)
        {
            try
            {
                // Lire les données d'un client
                Clients unClient = new Clients
                (
                    Lecture.ReadInt32(),
                    Lecture.ReadString(),
                    Lecture.ReadString(),
                    Lecture.ReadString()
                );

                // Ajouter la fiche à la liste pour réécriture ultérieure
                clients.Add(unClient);

                if (compteurFiche == numeroFiche)
                {
                    ficheTrouvee = true;

                    // Afficher les informations actuelles
                    Console.WriteLine("Fiche trouvée. Données actuelles :");
                    Console.WriteLine($"Numéro : {unClient.NumClients}");
                    Console.WriteLine($"Nom : {unClient.NomClient}");
                    Console.WriteLine($"Prénom : {unClient.PrenomClient}");
                    Console.WriteLine($"Téléphone : {unClient.TelClient}");
                    Console.WriteLine("-------------------");

                    // Demander confirmation
                    Console.WriteLine("Confirmez-vous la suppression ? (O/N)");
                    string confirmation = Console.ReadLine().ToUpper();
                    if (confirmation == "O")
                    {
                        // Ajouter un '*' au début du nom
                        unClient.NomClient = "*" + unClient.NomClient;
                        clients[compteurFiche - 1] = unClient; // Mettre à jour la fiche dans la liste
                        Console.WriteLine("Fiche supprimée avec succès (suppression logique) !");
                    }
                    else
                    {
                        Console.WriteLine("Suppression annulée.");
                    }
                }

                compteurFiche++;
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
        Console.WriteLine("Fiche introuvable.");
        return;
    }

    // Réécriture complète du fichier binaire avec la fiche mise à jour
    using (FileStream MonFichier = new FileStream("Clients.bin", FileMode.Create, FileAccess.Write))
    using (BinaryWriter Ecriture = new BinaryWriter(MonFichier))
    {
        foreach (Clients client in clients)
        {
            Ecriture.Write(client.NumClients);
            Ecriture.Write(client.NomClient);
            Ecriture.Write(client.PrenomClient);
            Ecriture.Write(client.TelClient);
        }
    }
}
//option 7
static void RecupererFiche()
{
    if (!File.Exists("Clients.bin"))
    {
        Console.WriteLine("Le fichier n'existe pas.");
        return;
    }

    Console.Write("Entrez le nom du client supprimé (avec *) : ");
    string nomSupprime = Console.ReadLine();

    if (!nomSupprime.StartsWith("*"))
    {
        Console.WriteLine("Le nom doit commencer par un astérisque (*) pour indiquer une suppression logique.");
        return;
    }

    bool ficheTrouvee = false; // Indique si la fiche demandée existe
    List<Clients> clients = new List<Clients>(); // Stocker temporairement toutes les fiches

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
                    Lecture.ReadInt32(),
                    Lecture.ReadString(),
                    Lecture.ReadString(),
                    Lecture.ReadString()
                );

                // Ajouter la fiche à la liste pour réécriture ultérieure
                clients.Add(unClient);

                // Vérifier si la fiche correspond au nom supprimé
                if (unClient.NomClient == nomSupprime)
                {
                    ficheTrouvee = true;

                    // Afficher les informations actuelles
                    Console.WriteLine("Fiche trouvée. Données actuelles :");
                    Console.WriteLine($"Numéro : {unClient.NumClients}");
                    Console.WriteLine($"Nom : {unClient.NomClient}");
                    Console.WriteLine($"Prénom : {unClient.PrenomClient}");
                    Console.WriteLine($"Téléphone : {unClient.TelClient}");
                    Console.WriteLine("-------------------");

                    // Demander un nouveau nom
                    Console.Write("Entrez le nouveau nom pour récupérer cette fiche : ");
                    string nouveauNom = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(nouveauNom))
                    {
                        Console.WriteLine("Le nouveau nom ne peut pas être vide. Annulation de la récupération.");
                        return;
                    }

                    // Mettre à jour le nom du client
                    unClient.NomClient = nouveauNom.ToUpper();
                    clients[clients.Count - 1] = unClient; // Mettre à jour la fiche dans la liste
                    Console.WriteLine("Fiche récupérée avec succès !");
                }
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
        Console.WriteLine("Aucune fiche correspondante trouvée.");
        return;
    }

    // Réécriture complète du fichier binaire avec la fiche mise à jour
    using (FileStream MonFichier = new FileStream("Clients.bin", FileMode.Create, FileAccess.Write))
    using (BinaryWriter Ecriture = new BinaryWriter(MonFichier))
    {
        foreach (Clients client in clients)
        {
            Ecriture.Write(client.NumClients);
            Ecriture.Write(client.NomClient);
            Ecriture.Write(client.PrenomClient);
            Ecriture.Write(client.TelClient);
        }
    }
}
//option 8
//utilisé l'option 3
//option 9
static void CompresserFichier()
{
    string cheminFichierOriginal = "Clients.bin";
    string cheminFichierTemporaire = "Clients_temp.bin";

    if (!File.Exists(cheminFichierOriginal))
    {
        Console.WriteLine("Le fichier n'existe pas.");
        return;
    }

    // Création du fichier temporaire pour stocker les données non supprimées
    using (FileStream fichierTemporaire = new FileStream(cheminFichierTemporaire, FileMode.Create, FileAccess.Write))
    using (BinaryWriter ecritureTemporaire = new BinaryWriter(fichierTemporaire))
    {
        using (FileStream fichierOriginal = new FileStream(cheminFichierOriginal, FileMode.Open, FileAccess.Read))
        using (BinaryReader lectureOriginale = new BinaryReader(fichierOriginal))
        {
            while (fichierOriginal.Position < fichierOriginal.Length)
            {
                try
                {
                    // Lire les données d'un client depuis le fichier original
                    Clients unClient = new Clients
                    (
                        lectureOriginale.ReadInt32(),
                        lectureOriginale.ReadString(),
                        lectureOriginale.ReadString(),
                        lectureOriginale.ReadString()
                    );

                    // Si la fiche n'est pas supprimée (pas de * au début du nom)
                    if (!unClient.NomClient.StartsWith("*"))
                    {
                        // Écrire la fiche non supprimée dans le fichier temporaire
                        ecritureTemporaire.Write(unClient.NumClients);
                        ecritureTemporaire.Write(unClient.NomClient);
                        ecritureTemporaire.Write(unClient.PrenomClient);
                        ecritureTemporaire.Write(unClient.TelClient);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la lecture d'un client : " + ex.Message);
                    break;
                }
            }
        }
    }

    // Supprimer le fichier original
    File.Delete(cheminFichierOriginal);

    // Renommer le fichier temporaire pour qu'il prenne le nom du fichier original
    File.Move(cheminFichierTemporaire, cheminFichierOriginal);

    Console.WriteLine("Le fichier a été compressé avec succès. Les fiches supprimées logiquement ont été supprimées.");
}
