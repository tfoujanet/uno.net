using System.Collections.Generic;

namespace Uno
{
    public interface IPioche
    {
        List<Carte> ListeCartes { get; }

        void InitialiserCartes();

        void MelangerCartes();

        Carte TirerCarte();
    }
}