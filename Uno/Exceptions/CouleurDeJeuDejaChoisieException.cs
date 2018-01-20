namespace Uno.Exceptions
{
    public class CouleurDeJeuDejaChoisieException : UnoException
    {
        public CouleurDeJeuDejaChoisieException()
         : base("La couleur du jeu a déjà été choisie")
         {}
    }
}