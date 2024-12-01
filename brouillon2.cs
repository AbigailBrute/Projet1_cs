/*static void ModifierClient()
{
    if (!File.Exists("Clients.bin"))
    {
        Console.WriteLine("Le fichier n'existe pas.");
        return;
    }

    Console.Write("Entrez le numéro de la fiche client à modifier : ");
    if (!int.TryParse(Console.ReadLine(), out int numeroFiche))
    {
        Console.WriteLine("La valeur saisie n'est pas un numéro valide.");
        return;
    }

    bool ficheTrouvee = false;

    using (FileStream monFichier = new FileStream("Clients.bin", FileMode.Open, FileAccess.ReadWrite))
    using (BinaryReader lecture = new BinaryReader(monFichier))
    using (BinaryWriter ecriture = new BinaryWriter(monFichier))
    {
        while (monFichier.Position < monFichier.Length)
        {
            try
            {
                // Lire les données du client
                long positionDebut = monFichier.Position;

                int numero = lecture.ReadInt32();
                string nom = lecture.ReadString();
                string prenom = lecture.ReadString();
                string telephone = lecture.ReadString();

                // Vérifiez si la fiche est marquée comme supprimée
                if (nom.StartsWith("*"))
                {
                    continue;
                }

                // Obtenir la position actuelle dans le fichier pour calculer le numéro de la fiche
                long Position = monFichier.Position;
                int NbElements = sizeof(int) + nom.Length + prenom.Length + telephone.Length;
                int numeroFicheActuelle = (int)(Position / NbElements);
                if (numeroFicheActuelle != numeroFiche)
                {
                    continue;
                }
                else
                {
                    ficheTrouvee = true;
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

        if(ficheTrouvee)
        {
            Console.WriteLine($"Fiche actuelle : {numeroFiche}");
                    Console.WriteLine($"Nom : {nom}");
                    Console.WriteLine($"Prénom : {prenom}");
                    Console.WriteLine($"Téléphone : {telephone}");
                    Console.WriteLine("-------------------");

                    // Demander les nouvelles données
                    Console.Write($"Nouveau nom ({nom}) : ");
                    string nouveauNom = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(nouveauNom)) nouveauNom = nom;

                    Console.Write($"Nouveau prénom ({prenom}) : ");
                    string nouveauPrenom = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(nouveauPrenom)) nouveauPrenom = prenom;

                    Console.Write($"Nouveau téléphone ({telephone}) : ");
                    string nouveauTelephone = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(nouveauTelephone)) nouveauTelephone = telephone;

                    // Repositionner pour écrire
                    monFichier.Seek(positionDebut, SeekOrigin.Begin);
                    ecriture.Write(numero);
                    ecriture.Write(nouveauNom);
                    ecriture.Write(nouveauPrenom);
                    ecriture.Write(nouveauTelephone);

                    Console.WriteLine("Données du client modifiées avec succès.");
        }
    }
}
*/

                
