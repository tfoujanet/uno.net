using System;
using System.Collections.Generic;
using System.Linq;
using Uno.Exceptions;
using Uno.Extensions;
using Uno.Interfaces;
using Uno.ValueObjects;

namespace Uno
{
    public class Partie
    {
        private const int NB_MIN_JOUEURS_PARTIE = 2;
        private const int NB_CARTE_MAIN_INITIALE = 7;

        public Partie()
        {
        }

        public event Action<Carte> CarteJouee;
        public void JouerCarte(Carte carte)
        {
            if (CarteJouee != null)
                CarteJouee(carte);
        }
    }
}