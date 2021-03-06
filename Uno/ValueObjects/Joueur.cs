using System.Collections.Generic;

namespace Uno.ValueObjects
{
    public class Joueur
    {
        public Joueur(string nom)
        {
            Nom = nom;
            Main = new List<Carte>();
        }

        public string Nom { get; }

        public List<Carte> Main { get; }

        public void TirerCarte(Carte carte)
        {
            Main.Add(carte);
        }
    }
}