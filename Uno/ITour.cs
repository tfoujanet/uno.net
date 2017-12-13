using System;

namespace Uno
{
    public interface ITour
    {
        event Action<Joueur, int> JoueurDoitPiocher;

        Sens Sens { get; set; }

        Joueur JoueurDuTour { get; }
    }
}