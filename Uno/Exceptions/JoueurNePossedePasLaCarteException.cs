namespace Uno.Exceptions
{
    public class JoueurNePossedePasLaCarteException : UnoException
    {
        public JoueurNePossedePasLaCarteException()
         : base("Ce n'est pas au tour de ce joueur")
         {}
    }
}