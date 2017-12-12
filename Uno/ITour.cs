namespace Uno
{
    public interface ITour
    {
        Sens Sens { get; set; }

        Joueur JoueurDuTour { get; }
    }
}