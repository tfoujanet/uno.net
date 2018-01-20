namespace Uno.Exceptions
{
    public class MauvaiseCouleurChoisieException: UnoException
    {
        public MauvaiseCouleurChoisieException()
            : base("Cette couleur ne peut pas être choisie")
        {            
        }
    }
}