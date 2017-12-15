namespace Uno.Exceptions
{
    public class MauvaisJoueurDeJouerException : UnoException
    {
        public MauvaisJoueurDeJouerException()
         : base("Ce n'est pas au tour de ce joueur")
         {}
    }
}