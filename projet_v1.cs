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
    public void Majuscule(ref string Nom)
    {
        Nom = Nom.ToUpper();
    }
    //Procédure pour convertir le prénom entré d'un client avec la première lettre en majuscule et le reste en minuscule
    public void FirstMajuscule(ref string Prenom)
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
    //Menu utilisateur
    public void Menu()
    {

    }

    static void Main()
    {
    }
}