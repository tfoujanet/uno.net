namespace Uno
{
    public interface IPile
    {
        Carte DerniereCarte { get; }

        Couleur? CouleurJeu { get; }

        Joueur JoueurChoixCouleur { get; }
    }
}