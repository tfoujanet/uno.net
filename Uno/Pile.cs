using System.Collections.Generic;

namespace Uno
{
    public class Pile
    {
        private List<Carte> pile = new List<Carte>();

        public Pile(Partie partie)
        {
            partie.CarteJouee += CarteJouee;
        }

        public Carte DerniereCarte
        {
            get { return pile.Count > 0 ? pile[pile.Count - 1] : null; }
        }

        public void CarteJouee(Carte carte)
        {
            pile.Add(carte);
        }
    }
}