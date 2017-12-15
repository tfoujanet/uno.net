namespace Uno.Exceptions
{
    public class CouleurDeJeuPasEncoreChoisieException : UnoException
    {
        public CouleurDeJeuPasEncoreChoisieException()
         : base("La couleur du jeu n'a pas encore été choisie")
         {}
    }
}