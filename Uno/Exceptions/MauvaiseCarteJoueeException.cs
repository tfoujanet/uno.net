namespace Uno.Exceptions
{
    public class MauvaiseCarteJoueeException : UnoException
    {
        public MauvaiseCarteJoueeException()
         : base("Cette carte ne peut pas être jouée")
         {}
    }
}