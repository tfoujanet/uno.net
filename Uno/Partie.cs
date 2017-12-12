using Uno.Exceptions;
using Uno.Extensions;

namespace Uno
{
    public delegate void CarteJoueeHandler(Carte carte);
    
    public class Partie
    {
        private readonly IPile pile;

        public Partie(IPile pile)
        {
            this.pile = pile;
        }

        public event CarteJoueeHandler CarteJouee;

        public void JouerCarte(Carte carte)
        {
            var derniereCarte = pile.DerniereCarte;
            if (!carte.EstSuperJoker() && !carte.EstJoker() && derniereCarte.Couleur != carte.Couleur && derniereCarte.Valeur != carte.Valeur)
                throw new MauvaiseCarteJoueeException();

            if (CarteJouee != null)
                CarteJouee(carte);
        }
    }
}