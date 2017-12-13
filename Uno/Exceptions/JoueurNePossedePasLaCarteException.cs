namespace Uno.Exceptions
{
    public class JoueurNePossedePasLaCarteException : UnoException
    {
        public JoueurNePossedePasLaCarteException()
         : base("Le joueur ne possède pas la carte dans sa main")
         {}
    }
}