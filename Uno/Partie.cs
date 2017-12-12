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
            this.CarteJouee += CarteEstJouee;

            Sens = Sens.Horaire;
        }

        public Sens Sens { get; set; }

        public event CarteJoueeHandler CarteJouee;

        public void JouerCarte(Carte carte)
        {
            var derniereCarte = pile.DerniereCarte;
            if (!carte.EstSuperJoker() && !carte.EstJoker() && derniereCarte.Couleur != carte.Couleur && derniereCarte.Valeur != carte.Valeur)
                throw new MauvaiseCarteJoueeException();

            if (CarteJouee != null)
                CarteJouee(carte);
        }

        private void CarteEstJouee(Carte carte)
        {
            if (carte.Valeur == Valeur.ChangementSens)
                InverserSens();
        }

        private void InverserSens()
        {
            Sens = Sens == Sens.Horaire ? Sens.Antihoraire : Sens.Horaire;
        }
    }
}