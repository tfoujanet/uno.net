using Uno.Exceptions;

namespace Uno
{
    public delegate void CarteJoueeHandler(Carte carte);
    
    public class Partie
    {
        private readonly Pile pile;

        public Partie()
        {
            pile = new Pile(this);
        }

        public event CarteJoueeHandler CarteJouee;

        public void JouerCarte(Carte carte)
        {
            var derniereCarte = pile.DerniereCarte;
            if (derniereCarte.Couleur != carte.Couleur && derniereCarte.Valeur != carte.Valeur)
                throw new MauvaiseCarteJoueeException();

            if (CarteJouee != null)
                CarteJouee(carte);
        }
    }
}