using System.Collections.Generic;
using System.Linq;
using Uno.ValueObjects;

namespace Uno.Helpers
{
    public static class PiochesExtensions
    {
        public static IEnumerable<Carte> CartesValeurs(this Pioche pioche, Valeur valeur)
        {
            return pioche.ListeCartes.Where(_ => _.Valeur == valeur);
        }

        public static IEnumerable<Carte> CartesCouleur(this Pioche pioche, Couleur couleur)
        {
            return pioche.ListeCartes.Where(_ => _.Couleur == couleur);
        }

        public static IEnumerable<Carte> CarteNumerotees(this IEnumerable<Carte> cartes)
        {
            var valeurBonus = new[] { Valeur.ChangementSens, Valeur.Plus2, Valeur.PasseTour, Valeur.Joker, Valeur.Plus4 };
            return cartes.Where(_ => !valeurBonus.Contains(_.Valeur));
        }
    }
}