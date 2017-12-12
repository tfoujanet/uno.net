namespace Uno.Exceptions
{
    public class PasAssezDeJoueurException : UnoException
    {
        public PasAssezDeJoueurException()
         : base("Il n'y a pas assez de joueurs pour commencer une partie")
         {}
    }
}