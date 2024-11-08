using System;
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
    static void Main()
    {
    }
}