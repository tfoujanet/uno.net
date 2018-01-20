namespace Uno.Exceptions
{
    public class JoueurDejaAjouteException : UnoException
    {
        public JoueurDejaAjouteException()
         : base("Ce joueur est déjà dans la partie")
         {}
    }
}