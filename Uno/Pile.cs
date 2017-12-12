using System.Collections.Generic;

namespace Uno
{
    public class Pile : IPile
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

        private void CarteJouee(Carte carte)
        {
            pile.Add(carte);
        }
    }
}