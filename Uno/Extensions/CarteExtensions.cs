using Uno.ValueObjects;

namespace Uno.Extensions
{
    public static class CarteExtensions
    {
        public static bool EstJoker(this Carte carte)
        {
            return carte.Valeur == Valeur.Joker && carte.Couleur == Couleur.Noir;
        }

        public static bool EstSuperJoker(this Carte carte)
        {
            return carte.Valeur == Valeur.Plus4 && carte.Couleur == Couleur.Noir;
        }

        public static bool EstNoire(this Carte carte)
        {
            return carte.EstJoker() || carte.EstSuperJoker();
        }
    }
}