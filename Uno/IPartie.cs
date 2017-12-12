using System;
using System.Collections.Generic;

namespace Uno
{
    public interface IPartie
    {
        List<Joueur> Joueurs { get; }        
        event Action<Carte> CarteJouee;
        event Action<Joueur> JoueurAjoute;
        event Action<IEnumerable<Joueur>> PartieCommencee;
    }
}