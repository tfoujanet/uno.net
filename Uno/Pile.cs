using System.Collections.Generic;

namespace Uno
{
    public class Pile : IPile
    {
        private List<Carte> pile = new List<Carte>();

        public Pile(IPartie partie)
        {
            partie.CarteJouee += CarteJouee;
            partie.CouleurChoisie += CouleurChoisie;
        }

        public Carte DerniereCarte
        {
            get { return pile.Count > 0 ? pile[pile.Count - 1] : null; }
        }

        public Couleur? CouleurJeu { get; private set; }

        public Joueur JoueurChoixCouleur { get; private set; }

        private void CarteJouee(Joueur joueur, Carte carte)
        {
            pile.Add(carte);
            CouleurJeu = carte.Couleur != Couleur.Noir ?
                carte.Couleur :
                (Couleur?) null;
            JoueurChoixCouleur = carte.Couleur == Couleur.Noir ? joueur : null;
        }

        private void CouleurChoisie(Couleur couleur)
        {            
            CouleurJeu = couleur;
            JoueurChoixCouleur = null;
        }
    }
}