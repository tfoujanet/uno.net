using System;
using System.Collections.Generic;
using System.Linq;
using Uno.Exceptions;
using Uno.Extensions;
using Uno.Interfaces;

namespace Uno
{
    public class Partie : IPartie
    {
        private const int NB_MIN_JOUEURS_PARTIE = 2;
        private const int NB_CARTE_MAIN_INITIALE = 7;
        private readonly ITalon talon;
        private readonly IPioche pioche;
        private readonly ITour tour;

        public Partie(ITalon talon, IPioche pioche, ITour tour)
        {
            this.talon = talon;
            this.pioche = pioche;
            this.tour = tour;
            this.PartieCommencee += PartieEstCommencee;
            this.CarteJouee += CarteJoueeParJoueur;
            this.JoueurAPioche += JoueurATireUneCarte;

            Joueurs = new List<Joueur>();
        }

        public List<Joueur> Joueurs { get; }

        public event Action<Joueur> JoueurAPioche;
        public event Action<Joueur, Carte> CarteJouee;
        public event Action<Joueur> JoueurAjoute;
        public event Action<IEnumerable<Joueur>> PartieCommencee;
        public event Action<Couleur> CouleurChoisie;

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
                PartieCommencee(Joueurs);
        }

        public void ChoisirCouleur(Joueur joueur, Couleur couleur)
        {
            if (talon.CouleurJeu.HasValue || talon.JoueurChoixCouleur == null)
                throw new CouleurDeJeuDejaChoisieException();

            if (talon.JoueurChoixCouleur != null && talon.JoueurChoixCouleur.Nom != joueur.Nom)
                throw new MauvaisJoueurDeJouerException();
            
            if (couleur == Couleur.Noir)
                throw new MauvaiseCouleurChoisieException();

            if (CouleurChoisie != null)
                CouleurChoisie(couleur);
        }

        public void PiocherCarte(Joueur joueur)
        {
            var joueurTour = tour.JoueurDuTour;
            if (joueurTour.Nom != joueur.Nom)
                throw new MauvaisJoueurDeJouerException();

            if (JoueurAPioche != null)
                JoueurAPioche(joueur);
        }

        public void JouerCarte(Joueur joueur, Carte carte)
        {
            var joueurTour = tour.JoueurDuTour;
            if (joueurTour.Nom != joueur.Nom)
                throw new MauvaisJoueurDeJouerException();

            var joueurQuiJoue = Joueurs.First(_ => _.Nom == joueur.Nom);
            if (joueurQuiJoue.Main.All(_ => _ != carte))
                throw new JoueurNePossedePasLaCarteException();

            var derniereCarte = talon.DerniereCarte;
            if (!derniereCarte.EstNoire() && !carte.EstSuperJoker() && !carte.EstJoker() && derniereCarte.Couleur != carte.Couleur && derniereCarte.Valeur != carte.Valeur)
                throw new MauvaiseCarteJoueeException();
            
            if (derniereCarte.EstNoire() && !talon.CouleurJeu.HasValue)
                throw new CouleurDeJeuPasEncoreChoisieException();

            if (derniereCarte.EstNoire() && talon.CouleurJeu.HasValue && carte.Couleur != talon.CouleurJeu)
                throw new MauvaiseCarteJoueeException();

            if (CarteJouee != null)
                CarteJouee(joueur, carte);
        }

        private void CarteJoueeParJoueur(Joueur joueur, Carte carte)
        {
            var joueurDuTour = Joueurs.First(_ => _.Nom == joueur.Nom);
            var carteJouee = joueurDuTour.Main.First(_ => _ == carte);
            joueurDuTour.Main.Remove(carteJouee);
        }

        private void PartieEstCommencee(IEnumerable<Joueur> joueurs)
        {
            pioche.MelangerCartes();
            DistribuerCartes();
        }

        private void JoueurATireUneCarte(Joueur joueur)
        {
            var carte = pioche.TirerCarte();
            var joueurDuTour = Joueurs.First(_ => _.Nom == joueur.Nom);
            joueurDuTour.TirerCarte(carte);

            try
            {
                JouerCarte(joueur, carte);
            }
            catch(UnoException)
            {
                tour.TerminerTour();
            }
        }

        private void DistribuerCartes()
        {
            for (var i=0; i<Joueurs.Count; i++)
            {
                for (var j=0; j< NB_CARTE_MAIN_INITIALE; j++)
                {
                    var carte = pioche.TirerCarte();
                    Joueurs[i].TirerCarte(carte);
                }
            }
        }
    }
}