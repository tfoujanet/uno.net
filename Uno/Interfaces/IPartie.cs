using System;
using System.Collections.Generic;
using Uno.ValueObjects;

namespace Uno.Interfaces
{
    public interface IPartie
    {
        List<Joueur> Joueurs { get; }
             
        event Action<Joueur, Carte> CarteJouee;
        event Action<Joueur> JoueurAjoute;
        event Action<IEnumerable<Joueur>> PartieCommencee;
        event Action<Couleur> CouleurChoisie;

        void AjouterJoueur(Joueur joueur);
    }
}