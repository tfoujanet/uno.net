using System.Collections.Generic;
using Uno.ValueObjects;

namespace Uno.Interfaces
{
    public interface IPioche
    {
        List<Carte> ListeCartes { get; }

        void InitialiserCartes();

        void MelangerCartes();

        Carte TirerCarte();
    }
}