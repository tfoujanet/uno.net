namespace Uno.Exceptions
{
    public abstract class UnoException : System.Exception
    {
        protected UnoException(string message)
         : base(message)
         {             
         }
    }
}