using System;
using System.Collections.Generic;
using Uno.Interfaces;
using Uno.ValueObjects;

namespace Uno
{
    public class Talon : ITalon
    {
        private List<Carte> talon = new List<Carte>();

        public Talon(IPartie partie, IPioche pioche)
        {
            partie.CarteJouee += CarteJouee;
            partie.CouleurChoisie += CouleurChoisie;
            partie.PartieCommencee += (joueurs) =>
            {
                CarteJouee(null, pioche.TirerCarte());
            };
        }

        public event Action<Carte> CartePosee;

        public Carte DerniereCarte
        {
            get { return talon.Count > 0 ? talon[talon.Count - 1] : null; }
        }

        public Couleur? CouleurJeu { get; private set; }

        public Joueur JoueurChoixCouleur { get; private set; }        

        private void CarteJouee(Joueur joueur, Carte carte)
        {
            talon.Add(carte);
            CartePosee?.Invoke(carte);
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