using System.Collections.Generic;
using Uno.Interfaces;
using Uno.ValueObjects;

namespace Uno
{
    public class Talon
    {
        private List<Carte> talon = new List<Carte>();

        public Talon(Partie partie)
        {
            partie.CarteJouee += CarteJouee;
        }

        public Carte DerniereCarte
        {
            get { return talon.Count > 0 ? talon[talon.Count - 1] : null; }
        }

        private void CarteJouee(Carte carte)
        {
            talon.Add(carte);
        }
    }
}