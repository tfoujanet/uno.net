using System;
using Uno.ValueObjects;

namespace Uno.Interfaces
{
    public interface ITour
    {
        event Action<Joueur, int> JoueurDoitPiocher;

        Sens Sens { get; set; }

        Joueur JoueurDuTour { get; }

        void TerminerTour();
    }
}