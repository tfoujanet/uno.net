using System;
using System.Collections.Generic;
using System.Linq;
using Uno.Interfaces;

namespace Uno
{
    public class Tour : ITour
    {
        private const int NB_CARTE_A_PIOCHE_PLUS_2 = 2;
        private const int NB_CARTE_A_PIOCHE_PLUS_4 = 4;
        private List<Joueur> joueurs;
        private int indexTourJoueur = 0;

        public event Action<Joueur, int> JoueurDoitPiocher;

        public Tour(IPartie partie)
        {
            partie.CarteJouee += CarteJouee;
            partie.PartieCommencee += InitialiserJoueurs;

            Sens = Sens.Horaire;
        }

        public Sens Sens { get; set; }

        public Joueur JoueurDuTour
        {
            get { return joueurs[indexTourJoueur]; }
        }

        public void TerminerTour()
        {
            IncrementerTour();
        }

        private void CarteJouee(Joueur joueur, Carte carte)
        {
            if (carte.Valeur == Valeur.ChangementSens)
                InverserSens();

            var listeValeursQuiPassentLeTour = new []
            {
                Valeur.PasseTour,
                Valeur.Plus2,
                Valeur.Plus4
            };
            if (listeValeursQuiPassentLeTour.Contains(carte.Valeur))
            {
                IncrementerTour();
                FairePiocherJoueur(carte.Valeur);
            }

            IncrementerTour();
        }

        private void InitialiserJoueurs(IEnumerable<Joueur> listeJoueurs)
        {
            joueurs = listeJoueurs.ToList();
            indexTourJoueur = 0;
        }

        private void InverserSens()
        {
            Sens = Sens == Sens.Horaire ? Sens.Antihoraire : Sens.Horaire;
        }

        private void FairePiocherJoueur(Valeur valeurCarte)
        {
            if (JoueurDoitPiocher != null && (valeurCarte == Valeur.Plus2 || valeurCarte == Valeur.Plus4))
            {
                var nbCarteAPiocher = valeurCarte == Valeur.Plus2 ? NB_CARTE_A_PIOCHE_PLUS_2 : NB_CARTE_A_PIOCHE_PLUS_4;
                JoueurDoitPiocher(JoueurDuTour, nbCarteAPiocher);
            }
        }

        private void IncrementerTour()
        {
            indexTourJoueur = indexTourJoueur + 1 == joueurs.Count ?
                0 :
                indexTourJoueur + 1;
        }
    }
}