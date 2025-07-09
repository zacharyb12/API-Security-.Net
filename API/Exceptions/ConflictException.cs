namespace API.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException() 
            : base ("Un conflit est arrivé veuillez reesayer avec des informations valides")
        {
            
        }

        public ConflictException(string message) 
            : base(message)
        {

        }
    }
}
