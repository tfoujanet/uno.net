using System;
using System.Collections.Generic;
using System.Linq;
using Uno.Helpers;
using Uno.Interfaces;
using Uno.ValueObjects;

namespace Uno
{

    public class Pioche : IPioche
    {
        private const int NB_CARTES_JOKERS = 4;
        private const int NB_CARTE_COULEUR = 2;

        private List<Carte> listeCartes = new List<Carte>();

        public Pioche()
        {
            InitialiserCartes();
        }

        public List<Carte> ListeCartes
        {
            get { return listeCartes; }
        }

        public void InitialiserCartes()
        {
            foreach (Couleur couleur in Enum.GetValues(typeof(Couleur)))
            {
                AjouterCartes(couleur);
            }
        }

        public void MelangerCartes()
        {
            listeCartes = listeCartes.Randomize().ToList();
        }

        public Carte TirerCarte()
        {
            var carte = listeCartes.First();
            listeCartes.Remove(carte);
            return carte;
        }

        private void AjouterCartes(Couleur couleur)
        {
            if (couleur == Couleur.Noir)
                AjouterJokers();
            else
                AjouterCarteCouleur(couleur);
        }

        private void AjouterCarteCouleur(Couleur couleur)
        {
            if (couleur == Couleur.Noir)
                return;

            listeCartes.Add(new Carte(Valeur.Zero, couleur));
            var listeValeurs = Enum.GetValues(typeof(Valeur)).Cast<Valeur>().Where(_ => _ != Valeur.Zero && _ != Valeur.Joker && _ != Valeur.Plus4);
            foreach (var valeur in listeValeurs)
            {
                for (var i = 0; i < NB_CARTE_COULEUR; i++)
                {
                    listeCartes.Add(new Carte(valeur, couleur));
                }
            }
        }

        private void AjouterJokers()
        {
            for (var i = 0; i < NB_CARTES_JOKERS; i++)
            {
                listeCartes.Add(new Carte(Valeur.Joker, Couleur.Noir));
                listeCartes.Add(new Carte(Valeur.Plus4, Couleur.Noir));
            }
        }
    }
}