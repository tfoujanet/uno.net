using System;
using System.Collections.Generic;
using Uno.ValueObjects;

namespace Uno.Interfaces
{
    public interface ITalon
    {
        event Action<Carte> CartePosee;

        Carte DerniereCarte { get; }

        Couleur? CouleurJeu { get; }

        Joueur JoueurChoixCouleur { get; }
    }
}