using System;
using System.Collections.Generic;
using System.Linq;
using Uno.Exceptions;
using Uno.Extensions;

namespace Uno
{
    public class Partie : IPartie
    {
        private const int NB_MIN_JOUEURS_PARTIE = 2;
        private const int NB_CARTE_MAIN_INITIALE = 7;
        private readonly IPile pile;
        private readonly IPioche pioche;
        private readonly ITour tour;

        public Partie(IPile pile, IPioche pioche, ITour tour)
        {
            this.pile = pile;
            this.pioche = pioche;
            this.tour = tour;
            this.PartieCommencee += PartieEstCommencee;

            Joueurs = new List<Joueur>();
        }

        public List<Joueur> Joueurs { get; }

        public event Action<Carte> CarteJouee;
        public event Action<Joueur> JoueurAjoute;
        public event Action PartieCommencee;

        public void AjouterJoueur(Joueur joueur)
        {
            if (Joueurs.Any(_ => _.Nom == joueur.Nom))
                throw new JoueurDejaAjouteException();

            Joueurs.Add(joueur);

            if (JoueurAjoute != null)
                JoueurAjoute(joueur);
        }

        public void CommencerPartie()
        {
            if (Joueurs.Count < NB_MIN_JOUEURS_PARTIE)
                throw new PasAssezDeJoueurException();

            if (PartieCommencee != null)
                PartieCommencee();
        }

        public void JouerCarte(Carte carte)
        {
            var derniereCarte = pile.DerniereCarte;
            if (!carte.EstSuperJoker() && !carte.EstJoker() && derniereCarte.Couleur != carte.Couleur && derniereCarte.Valeur != carte.Valeur)
                throw new MauvaiseCarteJoueeException();

            if (CarteJouee != null)
                CarteJouee(carte);
        }

        private void PartieEstCommencee()
        {
            pioche.MelangerCartes();
            DistribuerCartes();
        }

        private void DistribuerCartes()
        {
            for (var i=0; i<Joueurs.Count; i++)
            {
                for (var j=0; j< NB_CARTE_MAIN_INITIALE; j++)
                {
                    var carte = pioche.TirerCarte();
                    Joueurs[i].Main.Add(carte);
                }
            }
        }
    }
}