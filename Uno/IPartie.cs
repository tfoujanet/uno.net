using System;

namespace Uno
{
    public interface IPartie
    {
        event Action<Carte> CarteJouee;
        event Action<Joueur> JoueurAjoute;
        event Action PartieCommencee;
    }
}