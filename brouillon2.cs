/*
static void ModifierClient()
{
    if (!File.Exists("Clients.bin"))
    {
        Console.WriteLine("Le fichier n'existe pas.");
        return;
    }

    int NumeroFiche;
    Console.Write("Entrez le numéro de la fiche client à modifier : ");
    if (!int.TryParse(Console.ReadLine(), out NumeroFiche))
    {
        Console.WriteLine("La valeur saisie n'est pas un nombre entier valide.");
        return;
    }

    long PositionDebut = 0;
    bool FicheTrouvee = false;

    using (FileStream MonFichier = new FileStream("Clients.bin", FileMode.Open, FileAccess.ReadWrite))
    using (BinaryReader Lecture = new BinaryReader(MonFichier))
    using (BinaryWriter Ecriture = new BinaryWriter(MonFichier))
    {
        MonFichier.Seek(0, SeekOrigin.Begin);

        while (MonFichier.Position < MonFichier.Length)
        {
            try
            {
                // Lire les champs d'un client
                int Numero = Lecture.ReadInt32();
                string Nom = Lecture.ReadString();
                string Prenom = Lecture.ReadString();
                string Telephone = Lecture.ReadString();

                // Obtenir la position actuelle dans le fichier pour calculer le numéro de la fiche
                long Position = MonFichier.Position;
                int NbElements = sizeof(int) + Nom.Length + Prenom.Length + Telephone.Length;
                int NumFiche = (int)(Position / NbElements);

                // Vérifier si la fiche est marquée comme supprimée
                if (Nom.StartsWith("*"))
                {
                    continue;
                }

                if (NumFiche == NumeroFiche)
                {
                    FicheTrouvee = true;
                    PositionDebut = Position; // Stocker la position de début de la fiche
                    Console.WriteLine("Fiche actuelle : " + NumFiche);
                    Console.WriteLine("Numéro : " + Numero);
                    Console.WriteLine("Nom : " + Nom);
                    Console.WriteLine("Prénom : " + Prenom);
                    Console.WriteLine("Téléphone : " + Telephone);
                    Console.WriteLine("-------------------");
                    break; // Sortir de la boucle une fois la fiche trouvée
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

        if (FicheTrouvee)
        {
            // Demander à l'utilisateur les nouvelles valeurs
            Console.Write($"Nouveau nom ({Nom}) : ");
            string nouveauNom = Console.ReadLine();
            if (string.IsNullOrEmpty(nouveauNom))
                nouveauNom = Nom;

            Console.Write($"Nouveau prénom ({Prenom}) : ");
            string nouveauPrenom = Console.ReadLine();
            if (string.IsNullOrEmpty(nouveauPrenom))
                nouveauPrenom = Prenom;

            Console.Write($"Nouveau téléphone ({Telephone}) : ");
            string nouveauTelephone = Console.ReadLine();
            if (string.IsNullOrEmpty(nouveauTelephone))
                nouveauTelephone = Telephone;

            // Écrire les nouvelles données dans le fichier
            MonFichier.Seek(PositionDebut, SeekOrigin.Begin);
            Ecriture.Write(Numero);
            Ecriture.Write(nouveauNom);
            Ecriture.Write(nouveauPrenom);
            Ecriture.Write(nouveauTelephone);

            Console.WriteLine("Données du client modifiées avec succès.");
        }
        else
        {
            Console.WriteLine($"Aucune fiche trouvée avec le numéro {NumeroFiche}.");
        }
    }
}
*/
