using System;
using System.Collections.Generic;
using System.Linq;
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

            Joueurs = new List<Joueur>();
            Sens = Sens.Horaire;
        }

        public Sens Sens { get; set; }

        public List<Joueur> Joueurs { get; }

        public event CarteJoueeHandler CarteJouee;
        public event Action<Joueur> JoueurAjoute;

        public void AjouterJoueur(Joueur joueur)
        {
            if (Joueurs.Any(_ => _.Nom == joueur.Nom))
                throw new JoueurDejaAjouteException();

            Joueurs.Add(joueur);

            if (JoueurAjoute != null)
                JoueurAjoute(joueur);
        }

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