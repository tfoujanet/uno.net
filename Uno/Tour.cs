using System.Collections.Generic;
using System.Linq;

namespace Uno
{
    public class Tour : ITour
    {
        private List<Joueur> joueurs;
        private int indexTourJoueur = 0;

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

        private void CarteJouee(Joueur joueur, Carte carte)
        {
            if (carte.Valeur == Valeur.ChangementSens)
                InverserSens();
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
    }
}