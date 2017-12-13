namespace Uno
{
    public interface ITalon
    {
        Carte DerniereCarte { get; }

        Couleur? CouleurJeu { get; }

        Joueur JoueurChoixCouleur { get; }
    }
}